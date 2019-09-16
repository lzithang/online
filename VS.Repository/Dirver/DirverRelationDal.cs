using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.IRepository;


namespace VS.Repository
{
	public class DirverRelationDal : BaseRepository<DirverRelation>, IDirverRelationDal
	{
        /// <summary>
        /// 获取所有元件
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,object> GetUnitAll(int areaId,int mcId)
        {
            Dictionary<int, object> pairs = new Dictionary<int, object>();
            object obj = new { AreaId = areaId, McId = mcId };
            //电动机
            string sql = "SELECT * FROM  tb_dirver_motor WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(1, Db.Ado.SqlQuery<DirverMotor>(sql, obj));
            //齿轮
            sql = "SELECT * FROM  tb_dirver_gear WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(2, Db.Ado.SqlQuery<DirverGear>(sql, obj));
            //风机
            sql = "SELECT * FROM  tb_dirver_fan WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(3, Db.Ado.SqlQuery<DirverFan>(sql, obj));
            //泵
            sql = "SELECT * FROM  tb_dirver_pump WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(4, Db.Ado.SqlQuery<DirverPump>(sql, obj));
            //皮带
            sql = "SELECT * FROM  tb_dirver_belt WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(5, Db.Ado.SqlQuery<DirverBelt>(sql, obj));
            //变速齿轮箱
            sql = "SELECT * FROM  tb_dirver_shifting WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(6, Db.Ado.SqlQuery<DirverShifting>(sql, obj));
            //线速度
            sql = "SELECT * FROM  tb_dirver_linerSpeed WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(7, Db.Ado.SqlQuery<DirverLinerspeed>(sql, obj));
            //行星齿轮箱
            sql = "SELECT * FROM  tb_dirver_gear_planet WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(8, Db.Ado.SqlQuery<DirverGearPlanet>(sql, obj));
            ////轴频率
            sql = "SELECT * FROM  tb_dirver_roller WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(9, Db.Ado.SqlQuery<DirverRoller>(sql, obj));
            //滚动轴承
            sql = "SELECT * FROM  tb_dirver_bearing WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(11, Db.Ado.SqlQuery<DirverBearing>(sql, obj));
            //滑动轴承
            sql = "SELECT * FROM  tb_dirver_bearing_slide WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(12, Db.Ado.SqlQuery<DirverBearingSlide>(sql, obj));
            //液力耦合器
            sql = "SELECT * FROM  tb_dirver_fluidcoupling WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(13, Db.Ado.SqlQuery<DirverFluidcoupling>(sql, obj));
            return pairs;
        }
	}
}
