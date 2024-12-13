using MachineTest_RESTAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MachineTest_RESTAPI.Repository
{
    public class AssetManagementRepository : IAssetManagementRepository
    {
        private readonly MachineTestAssetMngtContext _context;

        public AssetManagementRepository(MachineTestAssetMngtContext context)
        {
            _context = context; //_context - virtua
        }

    //ASSET TYPE AND ASSET DEFINITION

        #region 1 -  Get all Asset types
        public async Task<ActionResult<IEnumerable<AssetType>>> GetAssetTypes()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AssetTypes.ToListAsync();
                }
                return new List<AssetType>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 2 -  Get all Asset Defs
        public async Task<ActionResult<IEnumerable<AssetDefinition>>> GetAssetDefinitions()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AssetDefinitions.Include(ad => ad.At).ToListAsync();  // At - AssetType At
                }
                return new List<AssetDefinition>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  3  - Insert an Asset type 
        public async Task<ActionResult<AssetType>> PostAssetTypeReturnRecord(AssetType assetType)
        {

            try
            {
                if (assetType == null)
                {
                    throw new ArgumentException(nameof(assetType), "AssetType data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }

                await _context.AssetTypes.AddAsync(assetType);

                await _context.SaveChangesAsync();

                var assetTypes = await _context.AssetTypes.FirstOrDefaultAsync(a => a.AtId == assetType.AtId);

                return assetTypes;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  4  - Insert an Asset  Def
        public async Task<ActionResult<AssetDefinition>> PostAssetDefinitionReturnRecord(AssetDefinition assetDef)
        {

            try
            {
                if (assetDef == null)
                {
                    throw new ArgumentException(nameof(assetDef), "AssetDefinition data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.AssetDefinitions.AddAsync(assetDef);

                await _context.SaveChangesAsync();

                var assetDefs = await _context.AssetDefinitions.Include(a => a.At) 
                    .FirstOrDefaultAsync(a => a.AdId == assetDef.AdId);

                return assetDefs;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 5  - Delete an Asset type
        public JsonResult DeleteTblAssetType(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid AtId"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                var existingAssetType = _context.AssetTypes.Find(id);

                if (existingAssetType == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "AssetType not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.AssetTypes.Remove(existingAssetType);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "AssetType Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion

        #region 6  - Delete an Asset def
        public JsonResult DeleteTblAssetDefinition(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid AdId"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                var existingAssetDefinition = _context.AssetDefinitions.Find(id);

                if (existingAssetDefinition == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "AssetDefinition not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.AssetDefinitions.Remove(existingAssetDefinition);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "AssetDefinition Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion


    //ASSET MASTER

        #region 1 -  Get all Asset Master data
        public async Task<ActionResult<IEnumerable<AssetMaster>>> GetAssetMasters()
        {
            try
            {
                if (_context != null)
                {
                    return await _context.AssetMasters.Include(a => a.Ad).Include(a => a.At).Include(a => a.Vd)
                        .ToListAsync();
                }
                return new List<AssetMaster>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  2  - Insert an Asset Master data 
        public async Task<ActionResult<AssetMaster>> PostTblAssetMasterReturnRecord(AssetMaster assetMaster)
        {
            try
            {
                if (assetMaster == null)
                {
                    throw new ArgumentNullException(nameof(assetMaster), "AssetMaster data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.AssetMasters.AddAsync(assetMaster);

                await _context.SaveChangesAsync();

                var recAssetMasters = await _context.AssetMasters.Include(a => a.Ad).Include(a => a.At).Include(a => a.Vd)
                    .FirstOrDefaultAsync(a => a.AmId == assetMaster.AmId);

                return recAssetMasters;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
        
        #endregion

        #region    3 - Insert an Asset Master data -return Id
        public async Task<ActionResult<int>> PostTblAssetMasterReturnId(AssetMaster assetMaster)
        {
            try
            {
                if (assetMaster == null)
                {
                    throw new ArgumentException(nameof(assetMaster), "AssetMaster data is null");
                }
                if (_context == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                await _context.AssetMasters.AddAsync(assetMaster);

                var changesRecord = await _context.SaveChangesAsync();

                if (changesRecord > 0)
                {
                    return assetMaster.AmId;
                }
                else
                {
                    throw new Exception("Failed to save AssetMaster record to the database");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region   4 - Get an Asset Master based on Id
        public async Task<ActionResult<AssetMaster>> GetAssetMastersById(int id)
        {
            try
            {
                if (_context != null)
                {
                    var employee = await _context.AssetMasters.Include(a => a.Ad).Include(a => a.At).Include(a => a.Vd)
                        .FirstOrDefaultAsync(a => a.AmId == id);
                    return employee;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  5  - Update an AssetMaster with ID 
        public async Task<ActionResult<AssetMaster>> PutTblAssetMaster(int id, AssetMaster assetsMaster)
        {
            try
            {
                if (assetsMaster == null)
                {
                    throw new InvalidOperationException("Database context is not initialized");
                }
                var existingAssetMaster = await _context.AssetMasters.FindAsync(id);
                if (existingAssetMaster == null)
                {
                    return null;
                }

                existingAssetMaster.AmModel = assetsMaster.AmModel;
                existingAssetMaster.AmSnumber = assetsMaster.AmSnumber;
                existingAssetMaster.AmMyyear = assetsMaster.AmMyyear;
                existingAssetMaster.AmPdate = assetsMaster.AmPdate;
                existingAssetMaster.AmWarranty = assetsMaster.AmWarranty;
                existingAssetMaster.AmFrom = assetsMaster.AmFrom;
                existingAssetMaster.AmTo = assetsMaster.AmTo;

                await _context.SaveChangesAsync();

                var assetrec = await _context.AssetMasters.Include(e => e.Ad).Include(e => e.At).Include(e => e.Vd)
                    .FirstOrDefaultAsync(existingAssetMaster => existingAssetMaster.AmId == assetsMaster.AmId);

                return assetrec;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  6  - Delete an AssetMaster with ID 
        public JsonResult DeleteTblAssetMaster(int id)
        {
            try
            {
                if (id <= null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Invalid AmId"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                if (_context == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "Database context is not initialized"
                    })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
                var existingAssetMaster = _context.AssetMasters.Find(id);

                if (existingAssetMaster == null)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        message = "AssetMaster not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                _context.AssetMasters.Remove(existingAssetMaster);

                _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    success = true,
                    message = "AssetMaster Deleted successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Database context is not initialized"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        #endregion
    }
}
