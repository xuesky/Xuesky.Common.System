using FreeSql.DataAnnotations;

namespace Xuesky.Common.DataAccess
{

    /// <summary>
    /// 用户角色中间表
    /// </summary>
    [Table(Name = "sys_user_role")]
    public partial class SysUserRole
    {

        /// <summary>
        /// 用户角色主键
        /// </summary>
        [Column(Name = "user_role_id", IsPrimary = true, IsIdentity = true, CanUpdate = false)]
        public int UserRoleId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Column(Name = "role_id")]
        public int RoleId
        {
            get => _RoleId; set
            {
                if (_RoleId == value) return;
                _RoleId = value;
                sys_role = null;
            }
        }
        private int _RoleId;

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column(Name = "user_id")]
        public int UserId
        {
            get => _UserId; set
            {
                if (_UserId == value) return;
                _UserId = value;
                sys_user = null;
            }
        }
        private int _UserId;

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(Name = "is_delete")]
        public bool IsDelete { get; set; } = false;


        #region 外键 => 导航属性，ManyToOne/OneToOne

        [Navigate("RoleId")]
        public virtual SysRole sys_role { get; set; }

        [Navigate("UserId")]
        public virtual SysUser sys_user { get; set; }

        #endregion

        #region 外键 => 导航属性，ManyToMany

        #endregion
    }

}
