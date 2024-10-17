using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskMasterAppBLL.Service.Implement;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppUI.Windows.UserWindows;

namespace TaskMasterAppUI.Windows.UserControls
{
    /// <summary>
    /// Interaction logic for AddCategoryModal.xaml
    /// </summary>
    public partial class AddCategoryModal : UserControl
    {
        private ITaskCategoryService _taskCategoryService = new TaskCategoryService();

        // Định nghĩa sự kiện
        public event Action CategoryAdded;

        public AddCategoryModal()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Category name cannot be empty!");
                return;
            }

            Category category = new Category() { CategoryName = categoryName };
            _taskCategoryService.AddCategory(category);
            MessageBox.Show($"Category '{categoryName}' added!");

            // Gọi sự kiện để thông báo rằng danh mục đã được thêm
            CategoryAdded?.Invoke();

            CategoryNameTextBox.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (Parent is Popup popup)
            {
                popup.IsOpen = false;
            }
        }
    }

}
