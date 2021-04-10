use master
drop database QLTRASUA
go
create database QLTRASUA
GO
use QLTRASUA
GO

CREATE TABLE KHACHHANG
(
	MaKH INT IDENTITY(1,1),
	HoTen nVarchar(50) NOT NULL,
	Taikhoan Varchar(50) UNIQUE,
	Matkhau Varchar(50) NOT NULL,
	Email Varchar(100) UNIQUE,
	DiachiKH nVarchar(200),
	DienthoaiKH Varchar(50),	
	Ngaysinh DATETIME
	CONSTRAINT PK_Khachhang PRIMARY KEY(MaKH)
)
GO
Create Table LOAI
(
	MaLoai int Identity(1,1),
	TenLoai nvarchar(50) NOT NULL,
	CONSTRAINT PK_Loai PRIMARY KEY(MaLoai)
)

GO
CREATE TABLE TRASUA
(
	MaTS INT IDENTITY(1,1),
	TenTS NVARCHAR(100) NOT NULL,
	Giaban Decimal(18,0) CHECK (Giaban>=0),
	Anhbia VARCHAR(50),
	MaLoai INT,
	CONSTRAINT PK_TraSua PRIMARY KEY(MaTS),
	CONSTRAINT FK_Loai FOREIGN KEY(MaLoai) REFERENCES LOAI(MaLoai),
)
GO
drop table CHITIETDONTHANG
drop table TRASUA
CREATE TABLE DONDATHANG
(
	MaDonHang INT IDENTITY(1,1),
	Dathanhtoan bit,
	Tinhtranggiaohang  bit,
	Ngaydat Datetime,
	Ngaygiao Datetime,	
	MaKH INT,
	CONSTRAINT FK_Khachhang FOREIGN KEY(MaKH) REFERENCES Khachhang(MaKH),
	CONSTRAINT PK_DonDatHang PRIMARY KEY(MaDonHang)
)
GO
CREATE TABLE CHITIETDONTHANG
(
	MaDonHang INT,
	MaTS INT,
	Soluong Int Check(Soluong>0),
	Dongia Decimal(18,0) Check(Dongia>=0),	
	CONSTRAINT PK_CTDatHang PRIMARY KEY(MaDonHang,MaTS),
	CONSTRAINT FK_MaTs FOREIGN KEY(MaTS) REFERENCES TRASUA(MaTS),
	CONSTRAINT FK_Madh FOREIGN KEY(Madonhang) REFERENCES dondathang(Madonhang),
)


GO
INSERT LOAI(TenLOAI) VALUES (N'Thức uống đặc biệt')
INSERT LOAI(TenLOAI) VALUES (N'Trà Sữa')
INSERT LOAI(TenLOAI) VALUES (N'Trà Nguyên Chất')
INSERT LOAI(TenLOAI) VALUES (N'Thức uống sáng tạo')
INSERT LOAI(TenLOAI) VALUES (N'Thức uống đá xay')
INSERT LOAI(TenLOAI) VALUES (N'Topping')

INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email)
VALUES (N'Dương Thành Phết', N'12 Trần Huy Liệu', N'0918062755', N'thayphet.net', N'123456', '08/20/1976', 'phetcm@hgmail.com')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Nguyễn Tiến Luân', N'21 Quận 6', N'0917654310', N'thang', N'123456', '10/15/1990', N'ntluan@hcmuns.edu.vn')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Đặng Quốc Hòa', N'32 Sư Vạn Hạnh', N'098713245', N'dqhoa', N'hoa', '05/21/1991', N'dqhoa@hcmuns.edu.vn')
INSERT KHACHHANG (Hoten, DiachiKH, DienthoaiKH, Taikhoan, Matkhau, Ngaysinh, Email) 
VALUES (N'Ngô Ngọc Ngân', N'12 Khu chung cư', N'0918544699', N'nnngan', N'ngan', '10/12/1986', N'nnngan@hcmuns.edu.vn')



INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà bí đao ',10000,'hot-xuat-hien-binh-tra-sua-con-ciu-hot-cuc-do-gay-bao-mang-xa-hoi-d6c8ec69636383223786147820.jpg',3)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà Đào ',10000,'hong_tra_vai_2.jpg',3)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Lục Trà ',10000,'ly-tra-sua-lam-tu-tra-den-huong-socola.jpg',3)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà sữa ',20000,'timthumb.jpg',2)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Lục trà sữa ',20000,'7-loai-tra-sua-8.jpg',2)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà sữa Thái  ',20000,'hong-tra-tran-chau-173012-1466605784.jpg',2)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà Thanos',200000,'tra-sua-hot-and-cold-menu.fid_.jpg',1)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà BatMan',200000,'5eb527f993bc3b290a698b6e429e1edf.jpg',1)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà Baron',200000,'fan-cuong-tra-sua-cung-chua-chac-biet-het-7-su-that-thu-vi-nay-3.jpg',1)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà Đen Machiato',5000,'slider-20171115-1510763558916839.jpg',4)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Hồng Trà Đen Machiato',5000,'tra-sua-tran-chau00021.png',4)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà Là Machiato',5000,'hot-xuat-hien-binh-tra-sua-con-ciu-hot-cuc-do-gay-bao-mang-xa-hoi-d6c8ec69636383223786147820.jpg',4)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Đá xay Chanh dây',50000,'ccf09c3c-6864-4040-836f-728abf505a84.jpg',5)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Đá xay Xoài',50000,'cover-tra-sua-thu-tuc-giay-to.jpg',5)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Đá xay Dưa Lưới',50000,'fan-cuong-tra-sua-cung-chua-chac-biet-het-7-su-that-thu-vi-nay-3.jpg',5)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà sữa hạt chia topping',47000,'hot-xuat-hien-binh-tra-sua-con-ciu-hot-cuc-do-gay-bao-mang-xa-hoi-d6c8ec69636383223786147820.jpg',6)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà sữa trân châu topping',47000,'5eb527f993bc3b290a698b6e429e1edf.jpg',6)
INSERT TRASUA (TENTS,GIABAN,ANHBIA,MALOAI)
VALUES (N'Trà sữa Phô mai topping',47000,'2 (1).jpg',6)

INSERT DONDATHANG (MaKH, Dathanhtoan,Ngaydat,Ngaygiao,Tinhtranggiaohang) 
VALUES ( 1,0, '10/15/2015', '10/20/2115',0)

INSERT DONDATHANG (MaKH, Dathanhtoan,Ngaydat,Ngaygiao,Tinhtranggiaohang) 
VALUES ( 2,0, '10/25/2015', '10/20/2115',0)

INSERT DONDATHANG (MaKH, Dathanhtoan,Ngaydat,Ngaygiao,Tinhtranggiaohang) 
VALUES ( 3,0, '10/05/2014', '10/20/2114',0)

INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (1, 18, 1, 25000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (1, 15, 3, 50000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (1, 14, 1, 25000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (2, 5, 3, 10000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (2, 9, 1, 15000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (2, 15, 3, 150000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (3, 9, 1, 25000)
INSERT CHITIETDONTHANG (MaDonHang,MaTS,SOLUONG, Dongia) VALUES (3, 10, 3,70000)

Create Table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
go
Insert into Admin values('admin','123456',N'Nguyễn Đắc Thịnh')
Insert into Admin values('user','654321',N'Đặng Chi Thái')