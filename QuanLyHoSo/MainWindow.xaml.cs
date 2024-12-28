using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace QuanLyHoSo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DatabaseHelper database = new DatabaseHelper();
            database.LoadData(dataGrid);
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Tạo và hiển thị cửa sổ nhập liệu
            AddHosoWindow addHosoWindow = new AddHosoWindow();
            if (addHosoWindow.ShowDialog() == true) // Kiểm tra nếu người dùng nhấn "Lưu"
            {
                HosoGanMoi newHoso = addHosoWindow.NewHoso; // Lấy đối tượng HosoGanMoi mới

                if (newHoso != null) // Kiểm tra xem newHoso có khác null không
                {
                    // Debugging: Kiểm tra giá trị của newHoso
                    MessageBox.Show("Hồ sơ đã được tạo!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Lấy ID có sẵn
                    DatabaseHelper database = new DatabaseHelper();
                    int newId = database.GetAvailableId(); // Gán ID cho hồ sơ mới

                    // Debugging: Kiểm tra ID mới
                    MessageBox.Show($"ID mới: {newId}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Thêm hồ sơ mới vào cơ sở dữ liệu
                    database.AddHoso(newHoso, newId); // Gọi phương thức AddHoso với ID

                    // Tải lại dữ liệu để cập nhật DataGrid
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Hồ sơ không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Tổng hợp theo HL"
        }

        private void ReadNumberButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Đọc số"
        }

        private void ProposalButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Tờ trình"
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Báo cáo"
        }

        private void TransferWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Chuyển kho"
        }

        private void NotSignedContractButton_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý chức năng "Chưa ký HĐ"
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại yêu cầu nhập STT
            string input = Microsoft.VisualBasic.Interaction.InputBox("Nhập STT hồ sơ muốn xóa:", "Xóa Hồ Sơ", "");

            if (int.TryParse(input, out int stt))
            {
                // Gọi phương thức xóa hồ sơ
                DeleteHosoBySTT(stt);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập một số hợp lệ cho STT.");
            }
        }

        private void DeleteHosoBySTT(int stt)
        {
            // Lấy danh sách dữ liệu từ DataGrid
            var data = dataGrid.ItemsSource as List<HosoGanMoi>;
            if (data != null)
            {
                // Tìm hồ sơ có STT tương ứng
                var hosoToDelete = data.FirstOrDefault(h => h.STT == stt); // Giả sử Id là STT

                if (hosoToDelete != null)
                {
                    // Xóa hồ sơ khỏi danh sách
                    data.Remove(hosoToDelete);
                    dataGrid.ItemsSource = null; // Đặt lại ItemsSource để cập nhật
                    dataGrid.ItemsSource = data; // Cập nhật lại ItemsSource

                    // Xóa hồ sơ khỏi cơ sở dữ liệu
                    DatabaseHelper database = new DatabaseHelper();
                    database.DeleteFromDatabase(hosoToDelete.STT);
                    MessageBox.Show("Xóa hồ sơ thành công.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hồ sơ với STT: " + stt);
                }
            }
        }
    }

    // Lớp dữ liệu
    

    
}