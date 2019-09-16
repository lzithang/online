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
        /// ��ȡ����Ԫ��
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,object> GetUnitAll(int areaId,int mcId)
        {
            Dictionary<int, object> pairs = new Dictionary<int, object>();
            object obj = new { AreaId = areaId, McId = mcId };
            //�綯��
            string sql = "SELECT * FROM  tb_dirver_motor WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(1, Db.Ado.SqlQuery<DirverMotor>(sql, obj));
            //����
            sql = "SELECT * FROM  tb_dirver_gear WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(2, Db.Ado.SqlQuery<DirverGear>(sql, obj));
            //���
            sql = "SELECT * FROM  tb_dirver_fan WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(3, Db.Ado.SqlQuery<DirverFan>(sql, obj));
            //��
            sql = "SELECT * FROM  tb_dirver_pump WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(4, Db.Ado.SqlQuery<DirverPump>(sql, obj));
            //Ƥ��
            sql = "SELECT * FROM  tb_dirver_belt WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(5, Db.Ado.SqlQuery<DirverBelt>(sql, obj));
            //���ٳ�����
            sql = "SELECT * FROM  tb_dirver_shifting WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(6, Db.Ado.SqlQuery<DirverShifting>(sql, obj));
            //���ٶ�
            sql = "SELECT * FROM  tb_dirver_linerSpeed WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(7, Db.Ado.SqlQuery<DirverLinerspeed>(sql, obj));
            //���ǳ�����
            sql = "SELECT * FROM  tb_dirver_gear_planet WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(8, Db.Ado.SqlQuery<DirverGearPlanet>(sql, obj));
            ////��Ƶ��
            sql = "SELECT * FROM  tb_dirver_roller WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(9, Db.Ado.SqlQuery<DirverRoller>(sql, obj));
            //�������
            sql = "SELECT * FROM  tb_dirver_bearing WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(11, Db.Ado.SqlQuery<DirverBearing>(sql, obj));
            //�������
            sql = "SELECT * FROM  tb_dirver_bearing_slide WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(12, Db.Ado.SqlQuery<DirverBearingSlide>(sql, obj));
            //Һ�������
            sql = "SELECT * FROM  tb_dirver_fluidcoupling WHERE area_id =@AreaId and mc_id=@McId";
            pairs.Add(13, Db.Ado.SqlQuery<DirverFluidcoupling>(sql, obj));
            return pairs;
        }
	}
}
