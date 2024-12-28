using System.Windows;

namespace QuanLyHoSo
{
    public partial class AddHosoWindow : Window
    {
        public HosoGanMoi NewHoso { get; private set; }

        public AddHosoWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Tạo một đối tượng HosoGanMoi mới
                NewHoso = new HosoGanMoi
                {
                    BangKe = txtBangKe.Text,
                    DotNhap = int.TryParse(txtDotNhap.Text, out int dotnhap) ? dotnhap : 0,
                    DBCu = txtDBCu.Text,
                    HDCu = txtHDCu.Text,
                    DBMoi = txtDBMoi.Text,
                    HDMoi = txtHDMoi.Text,
                    Dot = int.TryParse(txtDot.Text, out int dot) ? dot : 0,
                    GB = int.TryParse(txtGB.Text, out int gb) ? gb : 0,
                    BuDM = txtBuDM.Text,
                    HieuLuc = txtHieuLuc.Text,
                    MaHopDong = txtMaHopDong.Text,
                    DSLuu = int.TryParse(txtDSLuu.Text, out int dsluu) ? (int?)dsluu : null,
                    GhiChu = txtGhiChu.Text,
                    Quan = txtQuan.Text
                };

                // Debugging: Kiểm tra giá trị của NewHoso
                MessageBox.Show("Hồ sơ đã được tạo trong cửa sổ nhập liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Đóng cửa sổ và trả về kết quả
                DialogResult = true; // Đặt kết quả là true
                Close(); // Đóng cửa sổ
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hồ sơ: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}