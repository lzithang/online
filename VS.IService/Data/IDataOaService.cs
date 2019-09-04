using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace VS.IService
{
    public interface IDataOaService : IBaseService<DataOa>
    {
        /// <summary>
        /// ��ȡ���������� count������
        /// </summary>
        /// <param name="ids">����</param>
        /// <param name="count">����</param>
        /// <returns>�����</returns>
        ResultData GetDataOaByDirId(IdsModel ids, int count);

        /// <summary>
        /// ��ȡ���з���������һ������
        /// </summary>
        /// <returns>�����</returns>
        ResultData GetDataOaNew();

        /// <summary>
        /// ��ȡ���������з�������һ������
        /// </summary>
        /// <returns>�����</returns>
        ResultData GetDataOaNewByMcId(int areaId, int mcId);

        /// <summary>
        /// ��ȡ������������ֵ��ÿһ���Ǹ�����
        /// </summary>
        /// <param name="ids">����</param>
        /// <returns></returns>
        ResultData GetDataOaByDirId(IdsModel ids);

        /// <summary>
        /// ��ѯ�����µ�ʱ�������б�
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultData GetDataOaTimeList(IdsModel ids);
    }
}
