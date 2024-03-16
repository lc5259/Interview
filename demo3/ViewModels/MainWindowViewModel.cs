using Prism.Mvvm;
using System.Windows.Threading;
using System;
using System.Collections.ObjectModel;
using demo3.Models;
using Prism.Commands;
using System.Windows.Controls.Primitives;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using MaterialDesignColors;

namespace demo3.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private DispatcherTimer timer;
        private DateTime startTime;
        private string onLineTime;
        private string _title = "Prism Application";
        private string _searchText;

        public DelegateCommand<string> SortedCommand { get; set; }
        public DelegateCommand<UserInfo> EditCommand { get; set; }
        public DelegateCommand<UserInfo> DeleteCommand { get; set; }
        public DelegateCommand AddItemCommand { get; set; }
        public DelegateCommand<UserInfo> SaveCommand { get; set; }
        public bool IsEditMode { get; set; }

        public ObservableCollection<UserInfo> _userInfos;
        private ObservableCollection<UserInfo> originalUserInfos; //原始数据



        public MainWindowViewModel()
        {
            // 创建一个 DispatcherTimer 实例
            timer = new DispatcherTimer();
            // 设置时间间隔为1秒
            timer.Interval = TimeSpan.FromSeconds(1);
            // 绑定 Tick 事件处理程序
            timer.Tick += Timer_Tick;
            // 记录启动时间
            startTime = DateTime.Now;
            // 启动定时器
            timer.Start();

            //初始化userInfos 数据
            InitUserInfos();
            // 将原始数据拷贝到新的集合中
            originalUserInfos = new ObservableCollection<UserInfo>(UserInfos.ToList());
            //删除按钮功能
            DeleteCommand = new DelegateCommand<UserInfo>(Delete);
            //排序按钮功能，正序ASC，倒叙DESC
            SortedCommand = new DelegateCommand<string>(Sorted);
            //新增
            //AddCommand = new DelegateCommand<UserInfo>(Delete);
            AddItemCommand = new DelegateCommand(AddNewItem);
            SaveCommand = new DelegateCommand<UserInfo>(SaveItem);
            //编辑
            EditCommand = new DelegateCommand<UserInfo>(EditItem);
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// 联系人列表
        /// </summary>
        public ObservableCollection<UserInfo> UserInfos
        {
            get { return _userInfos; }
            set { _userInfos = value; RaisePropertyChanged(); }
        }
        /// <summary>
        /// 在线时间
        /// </summary>
        public string OnLineTime
        {
            get { return onLineTime; }
            set { onLineTime = value; RaisePropertyChanged(); }
        }

        //搜索
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged(nameof(SearchText));
                // 在此处执行查询操作
                Search();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 计算从启动到当前的时间间隔
            TimeSpan elapsedTime = DateTime.Now - startTime;

            // 更新页面上显示的计时时间
            OnLineTime = string.Format(" Online Time: {0:D2}:{1:D2}:{2:D2}",
                                    elapsedTime.Hours,
                                    elapsedTime.Minutes,
                                    elapsedTime.Seconds);
        }


        public void InitUserInfos()
        {
            UserInfos = new ObservableCollection<UserInfo>();
            UserInfos.Add(new UserInfo() { SortId = "D", Name = "David", Email = "David.wang@Crown.com", Address = "shanghai" });
            UserInfos.Add(new UserInfo() { SortId = "C", Name = "Crown", Email = "Crown.wang@perkinelmer.com", Address = "shanghai" });
            UserInfos.Add(new UserInfo() { SortId = "A", Name = "Aron", Email = "Aron.wang@perkinelmer.com", Address = "shanghai" });
            UserInfos.Add(new UserInfo() { SortId = "E", Name = "Eric", Email = "Eric.wang@perkinelmer.com", Address = "shanghai" });
            UserInfos.Add(new UserInfo() { SortId = "B", Name = "Brown", Email = "Brown.wang@perkinelmer.com", Address = "Crown City" });
        }

        public void Delete(UserInfo user)
        {
            try
            {
                if (user != null)
                {
                    UserInfos.Remove(user);
                    originalUserInfos.Remove(user);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Sorted(string sortedRule)
        {
            try
            {
                if (!string.IsNullOrEmpty(sortedRule))
                {
                    switch (sortedRule.ToUpperInvariant())
                    {
                        case "ASC":
                            UserInfos = new ObservableCollection<UserInfo>(UserInfos.OrderBy(u => u.SortId));
                            break;
                        case "DESC":
                            UserInfos = new ObservableCollection<UserInfo>(UserInfos.OrderByDescending(u => u.SortId));
                            break;
                        default:
                            // 默认按照正序排列
                            UserInfos = new ObservableCollection<UserInfo>(UserInfos.OrderBy(u => u.SortId));
                            break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Search()
        {
            try
            {
                // 如果搜索文本为空，则显示所有的用户信息
                if (string.IsNullOrEmpty(SearchText) || SearchText == "Enter keyword to search")
                {
                    // 显示所有用户信息
                    UserInfos = new ObservableCollection<UserInfo>(originalUserInfos);
                }
                else
                {
                    // 使用 LINQ 查询语句来过滤 UserInfos 列表
                    var filteredUserInfos = UserInfos.Where(u =>
                        u.SortId.Contains(SearchText, StringComparison.Ordinal) ||
                        u.Name.Contains(SearchText, StringComparison.Ordinal) ||
                        u.Email.Contains(SearchText, StringComparison.Ordinal) ||
                        u.Address.Contains(SearchText, StringComparison.Ordinal)
                    );

                    // 更新 UserInfos 列表以显示过滤后的结果
                    UserInfos = new ObservableCollection<UserInfo>(filteredUserInfos);
                }
            }
            catch (Exception)
            {
                // 可以在这里处理异常情况
                throw;
            }
        }

        /// <summary>
        /// 添加一个新的Item
        /// </summary>
        private void AddNewItem()
        {
            // 添加新的空 UserInfo 对象到集合中
            UserInfos.Add(new UserInfo());

            // 进入编辑模式
            IsEditMode = true;
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        private void SaveItem(UserInfo userInfo)
        {
            // 在这里保存编辑后的数据
            // 在 UserInfos 中查找指定 ID 的 UserInfo 对象
            var userInfoToUpdate = UserInfos.FirstOrDefault(u => u.Id == userInfo.Id);

            // 如果找到了指定的 UserInfo 对象，则更新其属性值
            if (userInfoToUpdate != null)
            {
                userInfoToUpdate.SortId = userInfo.Name[0].ToString().ToUpper();
                userInfoToUpdate.Name = userInfo.Name;
                userInfoToUpdate.Email = userInfo.Email;
                userInfoToUpdate.Address = userInfo.Address;
                // 触发 PropertyChanged 事件，通知界面更新
                RaisePropertyChanged(nameof(UserInfos));
                //当原始表中不存在时候才插入
                if (originalUserInfos.FirstOrDefault(u => u.Id == userInfoToUpdate.Id) == null)
                {
                    originalUserInfos.Add(userInfoToUpdate);
                }
            }
            int index = UserInfos.IndexOf(userInfo);
            if (index >= 0 && index < UserInfos.Count)
            {
                UserInfos[index] = userInfo;
                // 更新界面
                RaisePropertyChanged(nameof(UserInfos));
            }
            // 退出编辑模式
            IsEditMode = false;
            RaisePropertyChanged(nameof(IsEditMode));
            //为了将新的数据展示到页面上
            Search();
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        private void EditItem(UserInfo userInfo)
        {
            //获取索引
            int index = UserInfos.IndexOf(userInfo);
            //先remove后insert的目的是为了触发转换器。发现只有在向UserInfos添加一条数据的时候才会只触发那条数据的转换器
            //如果直接触发IsEditMode更新，会将全部的子控件触发转换器
            UserInfos.Remove(userInfo);
            UserInfos.Insert(index, userInfo);

            // 进入编辑模式
            IsEditMode = true;
            //RaisePropertyChanged(nameof(IsEditMode));
        }

    }
}
