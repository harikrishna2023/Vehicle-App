using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VehicleApp.API.Data;
using VehicleApp.API.Models.Domain;
using VehicleApp.API.Models.DTOs;
using VehicleApp.API.Repositories.IRepositories;

namespace VehicleApp.API.Repositories
{
    public class CategoryRepositorycs : ICategoryRepository
    {
        private readonly VehicleAppDBContext dbContext;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CategoryRepositorycs(VehicleAppDBContext _dbContext,
             IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = _dbContext;
            this._env = env;
            this.httpContextAccessor = httpContextAccessor;

        }
        


        public async Task<CategoryTemp?> DeleteCategory(int Id)
        {
            try
            {
                
                var existingCategory = await dbContext.categoryTemps.FirstOrDefaultAsync(x => x.category_id == Id);
                if (existingCategory != null)
                {
                    var vehicleData = await (from v in dbContext.vehicles
                                             where v.weight >= existingCategory.min_value &&
                                             v.weight <= existingCategory.max_value
                                             select v).FirstOrDefaultAsync();

                    if (vehicleData != null)
                    {
                        throw new Exception("There is already vehicle belong to category weight");
                    }
                }

                if (existingCategory == null)
                {
                    existingCategory = await dbContext.categoryTemps.FirstOrDefaultAsync(x => x.temp_id == Id);
                }

                if (existingCategory != null)
                {
                    existingCategory.action = "DELETE";
                    await dbContext.SaveChangesAsync();
                   
                }
                return existingCategory;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


       

        public async Task<List<CategoryTemp?>> ListCategory()
        {
            try
            {
                //deleting data from temp table
                var deleteData = await dbContext.categoryTemps.ToListAsync();

                if (deleteData != null)
                {
                    foreach (var item in deleteData)
                    {
                        dbContext.categoryTemps.Remove(item);
                    }
                }
                await dbContext.SaveChangesAsync();

                // var listData = new List<CategoryTemp>();

                var listData = await (from c in dbContext.categories
                                      select new CategoryTemp
                                      {
                                          action = "NEW",
                                          name = c.name,
                                          min_value = c.min_value,
                                          max_value = c.max_value,
                                          icon = c.icon,
                                          category_id = c.id

                                      }).ToListAsync();

                foreach (var item in listData)
                {
                    await dbContext.categoryTemps.AddAsync(item);

                }
                await dbContext.SaveChangesAsync();

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<CategoryTemp> AddCategoryTemp(AddCategoryTemp tempData)
        {
            var filePath = string.Empty;
            var urlPath = string.Empty;
            try
            {

                string folderName = "CategoryIconsTemp";

                string newPath = Path.Combine(_env.ContentRootPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                var imageName = Guid.NewGuid() + tempData.file.FileName;
                filePath = Path.Combine(newPath, imageName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    tempData.file.CopyTo(stream);
                }
                urlPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/CategoryIconsTemp/{imageName}";

                var categoryTempDomainModel = new CategoryTemp
                {
                    name = tempData.name,
                    min_value = tempData.min_value,
                    max_value = tempData.max_value,
                    icon = urlPath,
                    category_id = tempData.id,
                    action = "ADD"
                };

                await dbContext.categoryTemps.AddAsync(categoryTempDomainModel);
                await dbContext.SaveChangesAsync();

                return categoryTempDomainModel;

            }
            catch (Exception ex)
            {
                FileInfo iconImage = new FileInfo(filePath);
                if (iconImage.Exists)
                {
                    iconImage.Delete();
                }
                throw new Exception(ex.Message);
            }
        }


        public async Task<CategoryTemp> EditCategoryTemp(AddCategoryTemp tempData)
        {
            var urlPath = string.Empty;
            string icon = string.Empty;
            try
            {

                //adding file to temp folder if exists

                if (tempData.file != null)
                {
                    urlPath = AddFileToFolder(tempData.file);
                }
                //check its existing in temp table ,if not adding 

                var tempExists = await dbContext.categoryTemps.FirstOrDefaultAsync(x => x.category_id == tempData.id);
                if (tempExists == null)
                {
                    icon = urlPath;
                    var categoryTempDomainModel = new CategoryTemp
                    {
                        name = tempData.name,
                        min_value = tempData.min_value,
                        max_value = tempData.max_value,
                        icon = urlPath,
                        category_id = tempData.id,
                        action = "ADD"
                    };

                    await dbContext.categoryTemps.AddAsync(categoryTempDomainModel);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    icon = tempExists.icon;
                    tempExists.action = "EDIT";
                    tempExists.name = tempData.name;
                    tempExists.min_value = tempData.min_value;
                    tempExists.max_value = tempData.max_value;
                    tempExists.icon =(tempData.file==null)?tempExists.icon:urlPath;
                    tempExists.category_id = tempData.id;
                    await dbContext.SaveChangesAsync();

                }
                //mapping to categorytemp
                var TempDomainModel = new CategoryTemp
                {
                    name = tempData.name,
                    min_value = tempData.min_value,
                    max_value = tempData.max_value,
                    icon = icon,
                    category_id = tempData.id
                };
                return TempDomainModel;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> submitData()
        {
            var listCategoryTemp = await dbContext.categoryTemps.Where(x => x.action.ToUpper() != "DELETE").OrderBy(x => x.min_value).ToListAsync();
            int flag = 0;
            try
            {


                foreach (var item in listCategoryTemp)
                {
                    var otherTempItems = listCategoryTemp.Where(x => x.temp_id != item.temp_id).OrderBy(x=>x.min_value).ToList();
                    var categoryItem = otherTempItems.Where(x =>
                                            x.min_value <= item.max_value
                                            && item.min_value <= x.max_value
                                            ).FirstOrDefault();
                    if (categoryItem != null)
                    {
                        throw new Exception("Category weight overlapping with existing category - " + categoryItem.name);
                    }
                    var minCategoryItem = otherTempItems.Where(x => x.min_value == item.max_value + 1).FirstOrDefault();
                    if (minCategoryItem == null)
                    {
                        flag++;
                    }

                }
                if (flag >1)
                {
                    throw new Exception("Category should not have any gaps between weights");
                }
                else
                {
                    //deleting from table 

                    var deleteItems = await dbContext.categoryTemps.Where(x => x.action == "DELETE").ToListAsync();
                    if (deleteItems != null)
                    {
                        foreach (var item in deleteItems)
                        {
                            var dataToDelete = await dbContext.categories.Where(x => x.id == item.category_id).FirstOrDefaultAsync();

                            if (dataToDelete != null)
                            {
                                dbContext.Remove(dataToDelete);
                            }
                        }
                        await dbContext.SaveChangesAsync();
                    }
                   

                    //adding new categories

                    var addItems = await dbContext.categoryTemps.Where(x => x.action == "ADD").ToListAsync();
                    foreach (var item in addItems)
                    {
                        var dataToAdd = new Category
                        {
                            name = item.name,
                            min_value = item.min_value,
                            max_value = item.max_value,
                            icon = item.icon.Replace("CategoryIconsTemp", "CategoryIcons"),
                            created_on = DateTime.Now
                        };
                        await dbContext.categories.AddAsync(dataToAdd);
                        await dbContext.SaveChangesAsync();
                    }

                    //editing categories
                    var editItems = await dbContext.categoryTemps.Where(x => x.action == "EDIT").ToListAsync();
                    foreach (var item in editItems)
                    {
                        var dataToEdit = await dbContext.categories.Where(x => x.id == item.category_id).FirstOrDefaultAsync();
                        if (dataToEdit != null)
                        {
                            dataToEdit.name = item.name;
                            dataToEdit.min_value = item.min_value;
                            dataToEdit.max_value = item.max_value;
                            dataToEdit.icon = item.icon.Replace("CategoryIconsTemp", "CategoryIcons");
                            dataToEdit.updated_on = DateTime.Now;
                            await dbContext.SaveChangesAsync();
                        }

                    }
                    //saving images to original folder 
                    copyFileToFolder();

                    //deleting data from temp table
                    var deleteData =await dbContext.categoryTemps.ToListAsync();

                    if (deleteData != null)
                    {
                        foreach (var item in deleteData)
                        {
                            dbContext.categoryTemps.Remove(item);
                        }
                    }
                    await dbContext.SaveChangesAsync();

                }
                return await dbContext.categories.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string AddFileToFolder(IFormFile file)
        {
            var urlPath = string.Empty;
            var filePath = string.Empty;
            string folderName = "CategoryIconsTemp";

            string newPath = Path.Combine(_env.ContentRootPath, folderName);

            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            var imageName = Guid.NewGuid() + file.FileName;
            filePath = Path.Combine(newPath, imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            urlPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/CategoryIconsTemp/{imageName}";

            return urlPath;
        }

        private void copyFileToFolder()
        {
            try
            {


                string source = Path.Combine(_env.ContentRootPath, "CategoryIconsTemp");

                string targetPath = Path.Combine(_env.ContentRootPath, "CategoryIcons");


                foreach (var srcPath in Directory.GetFiles(source))
                {

                    File.Copy(srcPath, srcPath.Replace(source, targetPath), true);
                }
                //deleting from source
               
                foreach (var srcPath in Directory.GetFiles(source))
                {
                    FileInfo iconImage = new FileInfo(srcPath);
                    iconImage.Delete();
                }
               
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
