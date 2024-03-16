using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo3.Models
{
    public  class UserInfo : BindableBase
    {
        private string id = Guid.NewGuid().ToString();
        private string sortId;
        private string name;
        private string email;
        private string address;

		/// <summary>
		/// 主键ID
		/// </summary>
        public string Id
		{
			get { return id; }
			set { id = value; }
		}
		/// <summary>
		/// 排序的id，也就是页面上的A，B，C，D
		/// </summary>
		public string SortId
        {
			get { return sortId; }
			set { sortId = value; }
		}


		/// <summary>
		/// 姓名
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		/// <summary>
		/// email 地址
		/// </summary>
		public string Email
        {
			get { return email; }
			set { email = value; }
		}


		/// <summary>
		/// 地址
		/// </summary>
		public string Address
        {
			get { return address; }
			set { address = value; }
		}


	}
}
