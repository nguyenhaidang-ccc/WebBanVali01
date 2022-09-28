using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebBanVali.Models
{
    [MetadataTypeAttribute(typeof(tDanhMucSPMetaData))]
    public partial class tDanhMucSP
    {
        internal sealed class tDanhMucSPMetaData
        {
            [DisplayName("Mã sản phẩm")]
            [Required(ErrorMessage = "Vui Lòng nhập dữ liệu cho trường này!")]
            public string MaSP { get; set; }
            [DisplayName("Tên sản phẩm")]
            public string TenSP { get; set; }
            [DisplayName("Mã Chất Liệu")]
            public string MaChatLieu { get; set; }
            [DisplayName("Ngăn Laptop")]
            public string NganLapTop { get; set; }
            public string Model { get; set; }
            [DisplayName("Màu sắc")]
            public string MauSac { get; set; }
            [DisplayName("Mã Kích Thước")]
            public string MaKichThuoc { get; set; }
            [DisplayName("Cân Nặng")]
            public Nullable<double> CanNang { get; set; }
            [DisplayName("Độ Nới")]
            public Nullable<double> DoNoi { get; set; }
            [DisplayName("Mã Hàng SX")]
            public string MaHangSX { get; set; }
            [DisplayName("Mã Nước SX")]
            public string MaNuocSX { get; set; }
            [DisplayName("Mã Đặc Tính")]
            public string MaDacTinh { get; set; }
            [DisplayName("Website")]
            public string Website { get; set; }
            [DisplayName("Thời Gian Bảo Hành")]
            public Nullable<double> ThoiGianBaoHanh { get; set; }
            [DisplayName("Giới Thiệu")]
            public string GioiThieuSP { get; set; }
            [DisplayName("Giá")]
            [RegularExpression(@"^[0-9]+$", ErrorMessage ="Chi nhap so cho truong nay")]
            public Nullable<double> Gia { get; set; }
            [DisplayName("Chiết Khấu")]
            public Nullable<double> ChietKhau { get; set; }
            [DisplayName("Mã Loại")]
            public string MaLoai { get; set; }
            [DisplayName("Mã Đối Tượng")]
            public string MaDT { get; set; }
            [DisplayName("Ảnh")]
            [FileExtensions(Extensions =".jpg", ErrorMessage ="Chi nhap file jpeg")]
            public string Anh { get; set; }
        }

    }
}