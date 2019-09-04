using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;

namespace VS.IRepository
{
    public interface IDataOaDal : IBaseRepository<DataOa>
    {
        /// <summary>
        /// ��ȡ���������� count������
        /// </summary>
        /// <param name="ids">����</param>
        /// <param name="count">����</param>
        /// <returns>�����</returns>
        List<DataOa> GetDataOaByDirId(IdsModel ids, int count);

        /// <summary>
        /// ��ȡ���з���������һ������
        /// </summary>
        /// <returns>�����</returns>
        List<DataOa> GetDataOaNew();

        /// <summary>
        /// ��ȡ���������з�������һ������
        /// </summary>
        /// <returns>�����</returns>
        List<DataOa> GetDataOaNewByMcId(int areaId, int mcId);

        /// <summary>
        /// ��ȡ������������ֵ��ÿһ���Ǹ�����
        /// </summary>
        /// <param name="ids">����</param>
        /// <returns></returns>
        object GetDataOaByDirId(IdsModel ids);

        /// <summary>
        /// ��ѯ�����µ�ʱ�������б�
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<TimeModel> GetDataOaTimeList(IdsModel ids);
    }
}
