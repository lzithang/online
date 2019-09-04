using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class AreaDal : BaseRepository<Area>, IAreaDal
	{
        public object GetTreeAll()
        {
            string sqlArea = @"SELECT * FROM tb_area";
            string sqlMc = @"SELECT * FROM tb_machine";
            string sqlPar = @"SELECT * FROM tb_parameter";
            string sqlDir = @"SELECT * FROM tb_meterage";
            string sqlSam = @"SELECT * FROM tb_meterage_samplerate";
            List<Area> areaList = Db.Ado.SqlQuery<Area>(sqlArea);
            List<Machine> machineList = Db.Ado.SqlQuery<Machine>(sqlMc);
            List<Parameter> parameterList = Db.Ado.SqlQuery<Parameter>(sqlPar);
            List<Meterage> directionList = Db.Ado.SqlQuery<Meterage>(sqlDir);
            List<MeterageSamplerate> sampleList = Db.Ado.SqlQuery<MeterageSamplerate>(sqlSam);
            return new {areaList, machineList, parameterList, directionList, sampleList };
        }

        /// <summary>
        /// 获取路径名称
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="mcId"></param>
        /// <param name="parId"></param>
        /// <returns></returns>
        public PathName GetPathName(int areaId , int mcId, int parId)
        {
            string sql = $@"SELECT 
(SELECT area_name from tb_area WHERE area_id = {areaId}) AreaName,
(SELECT mc_name from tb_machine WHERE area_id = {areaId} and mc_id = {mcId}) McName,
(SELECT par_bearing from tb_parameter WHERE area_id = {areaId} and mc_id = {mcId} and par_id = {parId}) ParName";
            return Db.Ado.SqlQuerySingle<PathName>(sql);
        }
	}
}
