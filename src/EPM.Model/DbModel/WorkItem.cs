using EPM.Model.DbModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Model.DbModel
{
    public class WorkItem : BaseModel
    {
        /// <summary>
        /// 工作记录内容
        /// </summary>
        public string WorkContent {get; set;}

        /// <summary>
        /// 工作日期
        /// </summary>
        public DateTime RecordDate {get; set;}
    }
}
