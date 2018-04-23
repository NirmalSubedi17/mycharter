using System;
namespace MyCharter.Core.Entities
{
    public class UserModel
    {
        public string UserName
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

		public override string ToString()
		{
            return UserName;
		}
	}
}
