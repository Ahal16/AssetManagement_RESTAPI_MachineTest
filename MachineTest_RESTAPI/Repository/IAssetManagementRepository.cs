using MachineTest_RESTAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace MachineTest_RESTAPI.Repository
{
    public interface IAssetManagementRepository
    {
     
    //ASSET TYPE AND ASSET DEFINITION

        #region 1 -  Get all Asset types
        public Task<ActionResult<IEnumerable<AssetType>>> GetAssetTypes();
        #endregion

        #region 2 -  Get all Asset Defs
        public Task<ActionResult<IEnumerable<AssetDefinition>>> GetAssetDefinitions();
        #endregion

        #region  3  - Insert an Asset type 
        public Task<ActionResult<AssetType>> PostAssetTypeReturnRecord(AssetType assetType);
        #endregion

        #region  4  - Insert an Asset  Def
        public Task<ActionResult<AssetDefinition>> PostAssetDefinitionReturnRecord(AssetDefinition assetDef);
        #endregion

        #region 5  - Delete an Asset type
        public JsonResult DeleteTblAssetType(int id); 
        #endregion

        #region 6  - Delete an Asset def
        public JsonResult DeleteTblAssetDefinition(int id);
        #endregion

    //ASSET MASTER

        #region 1 -  Get all Asset Master data
        public Task<ActionResult<IEnumerable<AssetMaster>>> GetAssetMasters();
        #endregion

        #region  2  - Insert an Asset Master data 
        public Task<ActionResult<AssetMaster>> PostTblAssetMasterReturnRecord(AssetMaster assetMaster);
        #endregion

        #region    3 - Insert an Asset Master data -return Id
        public Task<ActionResult<int>> PostTblAssetMasterReturnId(AssetMaster assetMaster);
        #endregion

        #region   4 - Get an Asset Master based on Id
        public Task<ActionResult<AssetMaster>> GetAssetMastersById(int id);
        #endregion

        #region  5  - Update an AssetMaster with ID 
        public Task<ActionResult<AssetMaster>> PutTblAssetMaster(int id, AssetMaster assetsMaster);
        #endregion

        #region  6  - Delete an AssetMaster with ID 
        public JsonResult DeleteTblAssetMaster(int id);
        #endregion
    }
}
