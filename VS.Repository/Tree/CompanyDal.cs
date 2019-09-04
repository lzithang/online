using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class CompanyDal : BaseRepository<Company>, ICompanyDal
	{
        /// <summary>
        /// 获取公司下所有树形结构体数据
        /// </summary>
        /// <param name="cpId"></param>
        /// <returns></returns>
        public object GetTreeAll(int cpId)
        {
            string sqlFac = @"SELECT * from tb_factory WHERE cp_id =@CpId;";
            string sqlArea = @"
SELECT * FROM tb_area WHERE area_id in (
SELECT area_id from tb_area WHERE ft_id in(
SELECT ft_id from tb_factory WHERE cp_id =@CpId));";
            string sqlMc=@"
SELECT * FROM tb_machine WHERE area_id in (
SELECT area_id from tb_area WHERE ft_id in(
SELECT ft_id from tb_factory WHERE cp_id =@CpId));";
            string sqlPar = @"
SELECT * FROM tb_parameter WHERE area_id in (
SELECT area_id from tb_area WHERE ft_id in(
SELECT ft_id from tb_factory WHERE cp_id =@CpId));";
            string sqlDir = @"
SELECT * FROM tb_meterage WHERE area_id in (
SELECT area_id from tb_area WHERE ft_id in(
SELECT ft_id from tb_factory WHERE cp_id =@CpId));";
            string sqlSam= @"SELECT * FROM tb_meterage_samplerate WHERE area_id in (
SELECT area_id from tb_area WHERE ft_id in(
SELECT ft_id from tb_factory WHERE cp_id =@CpId));";
            List<Factory> factoryList = Db.Ado.SqlQuery<Factory>(sqlFac, new { CpId = cpId });
            List<Area> areaList = Db.Ado.SqlQuery<Area>(sqlArea, new { CpId = cpId });
            List<Machine> machineList = Db.Ado.SqlQuery<Machine>(sqlMc, new { CpId = cpId });
            List<Parameter> parameterList = Db.Ado.SqlQuery<Parameter>(sqlPar, new { CpId = cpId });
            List<Meterage> directionList = Db.Ado.SqlQuery<Meterage>(sqlDir, new { CpId = cpId });
            List<MeterageSamplerate> sampleList = Db.Ado.SqlQuery<MeterageSamplerate>(sqlSam, new { CpId = cpId });

            return new { factoryList,areaList,machineList,parameterList,directionList,sampleList};
        }
    }
}
