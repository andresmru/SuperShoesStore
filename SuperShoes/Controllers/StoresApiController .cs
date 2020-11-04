using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SuperShoes.Model;
using System.Linq.Expressions;

namespace SuperShoes.Controllers
{
    [Route("services/stores")]
    [ApiController]
    public class StoresApiController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoresApiController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<Response>> GetStores()
        {
            Response response = new Response();
            try
            {
                List<Store> StoreList = await _context.Stores.Where(p => p.StatusId < StatusIdCancelled).ToListAsync<Store>();

                response.Success = true;
                response.Component = StoreList;
            }
            catch (Exception ex)
            {
                response.ErrorCode = ResultServerError;
                response.ErrorMessage = ex.Message;
                return BadRequest(response);
            }
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetStore(int id)
        {
            Response response = new Response();
            Store store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "StoreId not found in database";
                return NotFound(response);
            }
            response.Success = true;
            response.Component = store;
            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutStore(int id, Store store)
        {
            Response response = new Response();

            if (id != store.StoreId)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "StoreId error";

                return BadRequest();
            }

            Store dbStore = await _context.Stores.Where(item => item.Name == store.Name && item.StoreId != store.StoreId && item.StatusId < StatusIdCancelled).FirstOrDefaultAsync();
            if (dbStore != null)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Store name already exists";
                return BadRequest(response);
            }

            _context.Entry<Store>(store).State = EntityState.Modified;

            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            return await GetStore(store.StoreId);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> PostStore(Store store)
        {
            Response response = new Response();
            Store dbStore = await _context.Stores.Where(item => item.Name == store.Name && item.StatusId < StatusIdCancelled).FirstOrDefaultAsync();
            if (dbStore != null)
            {
                response.ErrorCode = ResultBadRequest;
                response.ErrorMessage = "Store name already exists";
                return BadRequest(response);
            }
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    store.StatusId = StatusIdNew;
                    _context.Stores.Add(store);
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            return await GetStore(store.StoreId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteStore(int id)
        {
            Response response = new Response();
            Store Store = await _context.Stores.FindAsync((object)id);
            if (Store == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "StoreId not found";
                return NotFound(response);
            }
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Store.StatusId = StatusIdCancelled;
                    await _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultDBError;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    response.ErrorCode = ResultBadRequest;
                    response.ErrorMessage = ex.Message;
                    return BadRequest(response);
                }
            }
            response.Success = true;

            return response;
        }

        [HttpGet("{id}/existences")]
        public ActionResult<Response> GetInventory(int id)
        {
            Response response = new Response();

            IQueryable<ProductExistenceDTO> productExistences = _context.Set<ProductExistenceDTO>().FromSqlRaw("exec dbo.sp_ProductExistence @StoreId={0}", id);

            if (productExistences == null)
            {
                response.ErrorCode = ResultRecordNotFound;
                response.ErrorMessage = "Inventory for StoreId not found in database";
                return NotFound(response);
            }

            response.Success = true;
            response.Component = productExistences;

            return response;
        }
    }
}
