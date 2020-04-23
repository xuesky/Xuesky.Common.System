using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

    /// <summary>
    /// 学生基本信息表
    /// </summary>
    [Table(Name = "stu_info")]
    public partial class StuInfo
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Column(Name = "stu_id", IsPrimary = true, IsIdentity = true, CanUpdate = false)]
        public int StuId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        [Column(Name = "class_id")]
        public int? ClassId
        {
            get => _ClassId; set
            {
                if (_ClassId == value) return;
                _ClassId = value;
                class_info = null;
            }
        }
        private int? _ClassId;

        /// <summary>
        /// 删除
        /// </summary>
        [Column(Name = "is_delete")]
        public bool? IsDelete { get; set; } = false;

        /// <summary>
        /// 启用
        /// </summary>
        [Column(Name = "is_use")]
        public bool? IsUse { get; set; } = true;

        /// <summary>
        /// 性别
        /// </summary>
        [Column(Name = "stu_gender", DbType = "nchar(1)")]
        public string StuGender { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column(Name = "stu_mobile", DbType = "varchar(255)")]
        public string StuMobile { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column(Name = "stu_name", DbType = "varchar(255)")]
        public string StuName { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        [Column(Name = "stu_no", DbType = "varchar(255)")]
        public string StuNo { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        [Column(Name = "stu_score")]
        public int? StuScore { get; set; } = 0;


        #region 外键 => 导航属性，ManyToOne/OneToOne

        [Navigate("ClassId")]
        public virtual ClassInfo class_info { get; set; }

        #endregion

        #region 外键 => 导航属性，OneToMany

        [Navigate("StuId")]
        public virtual List<StuScoreLog> stu_score_logs { get; set; }

        [Navigate("StuId")]
        public virtual List<StuWechatInfo> stu_wechat_infos { get; set; }

        #endregion

        #region 外键 => 导航属性，ManyToMany

        #endregion
    }

}
