namespace 数据采集档案管理系统___加工版
{
    class UserHelper
    {
        private static User user;

        public static User GetUser()
        {
            if(user == null)
                user = new User();
            return user;
        }
    }
    class User
    {
        private object userId;
        private string userName;
        private string realName;
        private string userUnit;
        private string userSepical;
        /// <summary>
        /// 用户ID
        /// </summary>
        public object UserId { get => userId; set => userId = value; }
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserName { get => userName; set => userName = value; }
        /// <summary>
        /// 用户所属单位
        /// </summary>
        public string UserUnit { get => userUnit; set => userUnit = value; }
        /// <summary>
        /// 用户所属专项ID
        /// </summary>
        public string UserSepical { get => userSepical; set => userSepical = value; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get => realName; set => realName = value; }
    }
}
