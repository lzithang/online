using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace VS.IService
{
	public interface IDataTwService:IBaseService<DataTw>
	{
        /// <summary>
        /// ��ȡ�����µ�ʱ���б�����������
        /// </summary>
        /// <param name="parameter">����</param>
        /// <param name="ids">����</param>
        /// <returns>ʱ���б�(����Ӧ����)</returns>
        ResultData GetDataTwTimeList(string parameter, IdsModel ids);

        /// <summary>
        /// ��ȡ������ĳʱ�䲨��
        /// </summary>
        /// <param name="parameter">����</param>
        /// <param name="ids">����</param>
        /// <param name="time">ʱ��</param>
        /// <returns></returns>
        ResultData GetDataTwByDirId(string parameter, IdsModel ids, TimeModel time);

        /// <summary>
        /// ���벨��
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool InsertDataTw(DataTw dataTw, string tableName);
    }
}
