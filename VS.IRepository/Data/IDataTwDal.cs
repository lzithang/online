using System;
using System.Collections.Generic;
using System.Text;
using Model;
using VS.Common;

namespace VS.IRepository
{
    /// <summary>
    /// ����Ƶ�����ݲ�ӿ�
    /// </summary>
    public interface IDataTwDal : IBaseRepository<DataTw>
    {
        /// <summary>
        /// ��ȡ�����µ�ʱ���б�����������
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="ids">����</param>
        /// <returns>ʱ���б�(����Ӧ����)</returns>
        object GetDataTwTimeList(string tableName, IdsModel ids);

        /// <summary>
        /// ��ȡ������ĳʱ�䲨��
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="ids">����</param>
        /// <param name="time">ʱ��</param>
        /// <returns></returns>
        List<DataTw> GetDataTwByDirId(string tableName, IdsModel ids, TimeModel time);

        /// <summary>
        /// ���벨��
        /// </summary>
        /// <param name="dataTw"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool InsertDataTw(DataTw dataTw, string tableName);

    }
}
