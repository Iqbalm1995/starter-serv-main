
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Cms;
using starter_serv.Constant;
using starter_serv.Data;
using starter_serv.Helper;
using starter_serv.Model;
using starter_serv.Models;
using starter_serv.ViewModel;
using starter_serv.ViewModel.Base;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Dynamic.Core;

namespace starter_serv.DataProviders
{
    public class UsersDataProvider : IUsersDataProvider
    {
        private readonly DbMediaServicesContext _context;

        [ExcludeFromCodeCoverage]
        public UsersDataProvider(DbMediaServicesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<UsrUser>> List(string? search, int limit, int page)
        {
            List<UsrUser> result = new List<UsrUser>();

            string filter = "";
            if (search != null)
                filter = search.ToLower();

            var data = (from u in _context.UsrUsers
                        where ((EF.Functions.Like(u.Email.ToLower(), "%" + filter + "%")) ||
                                (EF.Functions.Like(u.Name.ToLower(), "%" + filter + "%")))
                        && u.IsDeleted.ToString() == ApplicationConstant.UNDELETED
                        select u).OrderByDescending(x => x.CreatedAt).AsQueryable();

            if (limit > 0)
            {
                result = await data.Skip(page).Take(limit).ToListAsync();
            }
            else
            {
                result = await data.ToListAsync();
            }

            return result;
        }

        public async Task<int> CountFilter(string? search)
        {
            int result = 0;

            string filter = "";
            if (search != null)
                filter = search.ToLower();

            result = await (from u in _context.UsrUsers
                            where ((EF.Functions.Like(u.Email.ToLower(), "%" + filter + "%")) ||
                                    (EF.Functions.Like(u.Name.ToLower(), "%" + filter + "%")))
                            && u.IsDeleted.ToString() == ApplicationConstant.UNDELETED
                            select u.Id).CountAsync();

            return result;
        }

        public async Task<UsrUser> GetById(int id)
        {
            UsrUser result = new UsrUser();

            result = await (from u in _context.UsrUsers
                            where u.Id == id
                            && u.IsDeleted.ToString() == ApplicationConstant.UNDELETED
                            select u).FirstOrDefaultAsync();
            return result;
        }
        public async Task<UsrUser> GetByEmail(string email)
        {
            UsrUser result = new UsrUser();

            result = await (from u in _context.UsrUsers
                            where u.Email == email
                            && u.IsDeleted.ToString() == ApplicationConstant.UNDELETED
                            select u).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ListPagedResults<UsrUser>> QueryPagedList(QueryPagedFilterModel filter)
        {
            ListPagedResults<UsrUser> result = new ListPagedResults<UsrUser>();

            // set global search string
            string searchString = "";
            if (filter.search != null)
                searchString = filter.search.ToLower();

            var data = (from u in _context.UsrUsers
                        where (
                            (EF.Functions.Like(u.Email.ToLower(), "%" + searchString + "%")) ||
                            (EF.Functions.Like(u.Name.ToLower(), "%" + searchString + "%"))
                        )
                        && u.IsDeleted.ToString() == ApplicationConstant.UNDELETED
                        select u).AsQueryable();

            // Use LINQ to filter the collection based on the parameter values:
            var filteredData = data;
            if (filter.FilterWhere.Any())
            {
                string OperatorWhere = "=";
                foreach (var _filter in filter.FilterWhere)
                {
                    string stringSql = string.Format("{0}{1}{2}{3}{4}"
                                    , _filter.Field
                                    , ParameterHeper.GetComparisonOperator(OperatorWhere)
                                    , ParameterHeper.IsUseDoubleQuote(_filter.Value) ? "\"" : ""
                                    , ParameterHeper.GetValue(_filter.Value)
                                    , ParameterHeper.IsUseDoubleQuote(_filter.Value) ? "\"" : "");


                    filteredData = filteredData.Where(stringSql);
                }
            }

            // set count total
            result.CountTotal = await filteredData.CountAsync();

            var sortData = filteredData;

            // set filter
            if (filter.OrderDir != null && filter.FieldOrder.Count > 0)
            {
                sortData = filteredData.OrderBy(
                    string.Format(
                        "{0} {1}"
                        , string.Join(",", filter.FieldOrder)
                        , filter.OrderDir
                    )
                );
            }

            // set paged list
            if (filter.limit > 0)
            {
                result.Data = await sortData.Skip(filter.page).Take(filter.limit).ToListAsync();
            }
            else
            {
                result.Data = await sortData.ToListAsync();
            }

            // set count
            result.Count = result.Data.Count();

            return result;
        }

        public async Task<string> AvatarWriteFile(IFormFile file)
        {
            string result = null;
            FileUploadOptions uploadOptions = new FileUploadOptions();

            string prefix = "US";
            string fileName = "";
            var fileSize = file.Length;
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            fileName = prefix + DateTime.Now.Ticks.ToString() + extension;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), uploadOptions.FilePathUploadAvatarUsers);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), uploadOptions.FilePathUploadAvatarUsers, fileName);
            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return result;
        }

        public async Task<ReturnViewModel> Insert(UsrUser data)
        {
            ReturnViewModel result = new ReturnViewModel();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.UsrUsers.AddAsync(data);

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    result.Status = true;
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    result.Status = false;
                    result.ExMessage = ex.Message;
                }
            }

            return result;
        }

        public async Task<ReturnViewModel> Update(UsrUser data)
        {
            ReturnViewModel result = new ReturnViewModel();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    result.Status = true;
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    result.Status = false;
                    result.ExMessage = ex.Message;
                }
            }

            return result;
        }
    }
}
