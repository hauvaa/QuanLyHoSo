using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace QuanLyHoSo
{
    public class DatabaseHelper
    {
        private string connectionString = "Data Source=Data/database.db;Version=3;";

        public void LoadData(DataGrid dataGrid)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Truy vấn dữ liệu từ bảng HOSO
                    string query = "SELECT bangke, dotnhap, dbcu, hdcu, dbmoi, hdmoi, dot, gb, budm, hieuluc, mahopdong, dsluu, ghichu, quan FROM HOSO";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    SQLiteDataReader reader = cmd.ExecuteReader();

                    List<HosoGanMoi> data = new List<HosoGanMoi>();
                    int stt = 1;

                    while (reader.Read())
                    {
                        data.Add(new HosoGanMoi
                        {
                            STT = stt++, // Tăng dần STT
                            BangKe = reader.IsDBNull(0) ? string.Empty : reader.GetString(0), // bangke
                            DotNhap = reader.IsDBNull(1) ? 0 : reader.GetInt32(1), // dotnhap
                            DBCu = reader.IsDBNull(2) ? string.Empty : reader.GetString(2), // dbcu
                            HDCu = reader.IsDBNull(3) ? string.Empty : reader.GetString(3), // hdcu
                            DBMoi = reader.IsDBNull(4) ? string.Empty : reader.GetString(4), // dbmoi
                            HDMoi = reader.IsDBNull(5) ? string.Empty : reader.GetString(5), // hdmoi
                            Dot = reader.IsDBNull(6) ? 0 : reader.GetInt32(6), // dot
                            GB = reader.IsDBNull(7) ? 0 : reader.GetInt32(7), // gb
                            BuDM = reader.IsDBNull(8) ? string.Empty : reader.GetString(8), // budm
                            HieuLuc = reader.IsDBNull(9) ? string.Empty : reader.GetString(9), // hieuluc
                            MaHopDong = reader.IsDBNull(10) ? string.Empty : reader.GetString(10), // mahopdong
                            DSLuu = reader.IsDBNull(11) ? (int?)null : reader.GetInt32(11), // dsluu
                            GhiChu = reader.IsDBNull(12) ? string.Empty : reader.GetString(12), // ghichu
                            Quan = reader.IsDBNull(13) ? string.Empty : reader.GetString(13) // quan
                        });
                    }

                    // Đưa dữ liệu vào DataGrid
                    dataGrid.ItemsSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        public void DeleteFromDatabase(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM HOSO WHERE id = @id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery(); // Lấy số hàng bị ảnh hưởng
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công hàng có ID: " + id);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hàng với ID: " + id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                }
            }
        }

        public int GetAvailableId()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                // Truy vấn để lấy tất cả các ID đã tồn tại
                string query = "SELECT Id FROM HOSO ORDER BY Id;";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                // Khởi tạo biến để theo dõi ID mong đợi
                int expectedId = 1; // Bắt đầu từ ID 1

                while (reader.Read())
                {
                    int currentId = reader.GetInt32(0);

                    // Kiểm tra xem ID hiện tại có lớn hơn ID mong đợi không
                    while (expectedId < currentId)
                    {
                        // Nếu ID mong đợi nhỏ hơn ID hiện tại, có nghĩa là ID mong đợi là trống
                        return expectedId; // Trả về ID trống
                    }
                    expectedId++; // Tăng ID mong đợi lên 1
                }

                // Nếu không có ID trống, trả về ID lớn nhất + 1
                return expectedId; // expectedId sẽ là ID lớn nhất + 1
            }
        }

        public void AddHoso(HosoGanMoi hoso, int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                INSERT INTO HOSO (id, bangke, dotnhap, dbcu, hdcu, dbmoi, hdmoi, dot, gb, budm, hieuluc, mahopdong, dsluu, ghichu, quan)
                VALUES (@id, @bangke, @dotnhap, @dbcu, @hdcu, @dbmoi, @hdmoi, @dot, @gb, @budm, @hieuluc, @mahopdong, @dsluu, @ghichu, @quan);";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id); // Gán ID
                        cmd.Parameters.AddWithValue("@bangke", hoso.BangKe);
                        cmd.Parameters.AddWithValue("@dotnhap", hoso.DotNhap);
                        cmd.Parameters.AddWithValue("@dbcu", hoso.DBCu);
                        cmd.Parameters.AddWithValue("@hdcu", hoso.HDCu);
                        cmd.Parameters.AddWithValue("@dbmoi", hoso.DBMoi);
                        cmd.Parameters.AddWithValue("@hdmoi", hoso.HDMoi);
                        cmd.Parameters.AddWithValue("@dot", hoso.Dot);
                        cmd.Parameters.AddWithValue("@gb", hoso.GB);
                        cmd.Parameters.AddWithValue("@budm", hoso.BuDM);
                        cmd.Parameters.AddWithValue("@hieuluc", hoso.HieuLuc);
                        cmd.Parameters.AddWithValue("@mahopdong", hoso.MaHopDong);
                        cmd.Parameters.AddWithValue("@dsluu", hoso.DSLuu);
                        cmd.Parameters.AddWithValue("@ghichu", hoso.GhiChu);
                        cmd.Parameters.AddWithValue("@quan", hoso.Quan);

                        int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh và lấy số dòng bị ảnh hưởng

                        // Kiểm tra xem có dòng nào được thêm vào không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Lưu hồ sơ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Lưu hồ sơ không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi (có thể ghi log hoặc thông báo cho người dùng)
                    Console.WriteLine("Lỗi khi thêm hồ sơ: " + ex.Message);
                }
            }
        }

        public bool TestDatabaseConnection()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true; // K ết nối thành công
                }
                catch
                {
                    return false; // Kết nối không thành công
                }
            }
        }
    }
}
