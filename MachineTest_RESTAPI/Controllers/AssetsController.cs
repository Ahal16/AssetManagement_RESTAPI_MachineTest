using MachineTest_RESTAPI.Model;
using MachineTest_RESTAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MachineTest_RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AssetsController : ControllerBase
    {
        private readonly IAssetManagementRepository _repository;

        //DI - Dependency Injection
        public AssetsController(IAssetManagementRepository repository)
        {
            _repository = repository;
        }


     //ASSET TYPE AND ASSET DEFINITION

        #region 1 -  Get all Asset types
        [HttpGet("at")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AssetType>>> GetAllAssetTypes()
        {
            var assets = await _repository.GetAssetTypes();
            if (assets == null)
            {
                return NotFound("No Asset Types found");
            }
            return Ok(assets);
        }
        #endregion

        #region  2 -  Get all Asset Defs
        [HttpGet("ad")]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AssetDefinition>>> GetAllAssetDefinitions()
        {
            var assets = await _repository.GetAssetDefinitions();
            if (assets == null)
            {
                return NotFound("No Asset Definitions found");
            }
            return Ok(assets);
        }
        #endregion

        #region   3  - Insert an Asset type 
        [HttpPost("at")]
        public async Task<ActionResult<AssetType>> InsertAssetTypesReturnRecord(AssetType assetT)
        {
            if (ModelState.IsValid)
            {
                var newAssetType = await _repository.PostAssetTypeReturnRecord(assetT);
                if (newAssetType != null)
                {
                    return Ok(newAssetType);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region   4  - Insert an Asset  Def
        [HttpPost("ad")]
        public async Task<ActionResult<AssetDefinition>> InsertAssetDefinitionsReturnRecord(AssetDefinition asetdefi)
        {
            if (ModelState.IsValid)
            {
                var newasetdefi = await _repository.PostAssetDefinitionReturnRecord(asetdefi);
                if (newasetdefi != null)
                {
                    return Ok(newasetdefi);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region 5  - Delete an Asset type
        [HttpDelete("at/{id}")]
        public IActionResult DeletefromTblAssetType(int id)
        {
            try
            {
                var result = _repository.DeleteTblAssetType(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "AssetType could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

        #region 6  - Delete an Asset Def
        [HttpDelete("ad/{id}")]
        public IActionResult DeletefromAssetDefinition(int id)
        {
            try
            {
                var result = _repository.DeleteTblAssetDefinition(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "AssetDefinition could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion


    //ASSET MASTER

        #region 1 -  Get all Asset master data
        [HttpGet("am")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<IEnumerable<AssetMaster>>> GetAllAssetMasters()
        {
            var assets = await _repository.GetAssetMasters();
            if (assets == null)
            {
                return NotFound("No Asset data found");
            }
            return Ok(assets);
        }
        #endregion

        #region   2  - Insert an Asset Master data

        [HttpPost("post")]
        public async Task<ActionResult<AssetMaster>> PostAssetMasterReturnRecord(AssetMaster assetsMast)
        {
            if (ModelState.IsValid)
            {
                var newAsset = await _repository.PostTblAssetMasterReturnRecord(assetsMast);
                if (newAsset != null)
                {
                    return Ok(newAsset);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }

       
        #endregion

        #region    3 - Insert an Asset Master data -return Id
        [HttpPost("v1")]
        public async Task<ActionResult<int>> InsertAssetMastersReturnId(AssetMaster assetsMaster)
        {
            if (ModelState.IsValid)
            {
                var newMasterId = await _repository.PostTblAssetMasterReturnId(assetsMaster);
                if (newMasterId != null)
                {
                    return Ok(newMasterId);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region  4  - Update an AssetMaster with ID 
        [HttpPut("am/{id}")]
        public async Task<ActionResult<int>> UpdateAssetMasterReturnRecord(int id, AssetMaster assetMaster)
        {
            if (ModelState.IsValid)
            {
                var updateAssetMaster = await _repository.PutTblAssetMaster(id, assetMaster);
                if (updateAssetMaster != null)
                {
                    return Ok(updateAssetMaster);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        #endregion

        #region   5  - Delete an AssetMaster with ID 
        [HttpDelete("am/{id}")]
        public IActionResult DeleteAssetMaster(int id)
        {
            try
            {
                var result = _repository.DeleteTblAssetMaster(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "AssetMaster could not be deleted or not found"
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { success = false, message = "An unexpected error occurs" });
            }
        }
        #endregion

    }
}
