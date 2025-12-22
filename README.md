# PRN222 - Programming with C#.NET

## 📚 Giới thiệu

Repository này chứa toàn bộ bài tập và bài thi thực hành của môn **PRN222 - Programming with C#.NET** tại trường FPT University.

## 📂 Cấu trúc thư mục

```
PRN222/
├── Account/                                          # Module quản lý tài khoản
├── FA25_PRN222_3W1_ASM1_SE180672_VietN/            # Assignment 1 - Razor Pages
├── FA25_PRN222_3W1_ASM2_SE180672_VietN/            # Assignment 2 - Worker Service
├── FA25_PRN222_3W1_ASM3_SE180672_VietN/            # Assignment 3 - MVC Web App
├── FA25_PRN222_3W1_ASM4_SE180672_VietN/            # Assignment 4 - Blazor Web App
├── FA25_PRN222_3W1_ASM5_SE180672_VietN/            # Assignment 5 - Lion Pet Management
├── PE_PRN222_FA25_NguyenViet/                       # Practical Exam FA25
├── PE_PRN222_SU25_TrialTest_NguyenViet/            # Trial Test SU25
└── PE__PRN222_FA25_00000000_NguyenViet/            # Practice Exam
```

## 🎯 Nội dung các Assignment

### Assignment 1: FFH Request System - Razor Pages
- Xây dựng ứng dụng quản lý yêu cầu sử dụng ASP.NET Core Razor Pages
- Áp dụng Repository Pattern và Service Layer
- CRUD operations với Entity Framework Core

### Assignment 2: FFH Request System - Worker Service
- Triển khai Background Service
- Xử lý tác vụ định kỳ và background tasks
- Integration với existing database

### Assignment 3: FFH Request System - MVC Web App
- Phát triển ứng dụng web sử dụng ASP.NET Core MVC
- Implement Model-View-Controller pattern
- Authentication và Authorization

### Assignment 4: FFH Request System - Blazor Web App
- Xây dựng interactive web UI với Blazor
- Component-based architecture
- Real-time updates và interactive features

### Assignment 5: Lion Pet Management System
- Hệ thống quản lý thú cưng
- Full-stack application với .NET
- Advanced features implementation

## 📝 Practical Exams

### PE_PRN222_FA25
Đề thi thực hành chính thức kỳ Fall 2025
- **Đề bài:** Charging Management System
- **Công nghệ:** ASP.NET Core Razor Pages
- **Thời gian:** 90 phút

### PE_PRN222_SU25_TrialTest
Đề thi thử kỳ Summer 2025
- **Đề bài:** Lion Pet Management
- **Công nghệ:** ASP.NET Core Razor Pages

### PE__PRN222_FA25_00000000
Đề luyện tập thực hành
- **Đề bài:** Real Estate Management
- **Công nghệ:** ASP.NET Core Razor Pages

## 🛠️ Công nghệ sử dụng

- **Framework:** ASP.NET Core 6.0/7.0/8.0
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **UI Frameworks:**
  - Razor Pages
  - MVC
  - Blazor
- **Architecture Patterns:**
  - Repository Pattern
  - Service Layer
  - Dependency Injection

## 🚀 Cách chạy project

### Yêu cầu

- Visual Studio 2022 hoặc Visual Studio Code
- .NET SDK 6.0 trở lên
- SQL Server 2019 hoặc SQL Server Express

### Các bước thực hiện

1. **Clone repository**
   ```bash
   git clone <repository-url>
   cd PRN222
   ```

2. **Chọn project muốn chạy**
   ```bash
   cd FA25_PRN222_3W1_ASM1_SE180672_VietN
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Update connection string**
   - Mở `appsettings.json`
   - Cập nhật connection string phù hợp với SQL Server của bạn

5. **Apply migrations** (nếu có)
   ```bash
   dotnet ef database update
   ```

6. **Run application**
   ```bash
   dotnet run
   ```

## 📖 Tài liệu tham khảo

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)

## 👨‍🎓 Thông tin sinh viên

- **Họ tên:** Nguyen Viet
- **MSSV:** SE180672
- **Lớp:** 3W1
- **Kỳ học:** Fall 2025

## 📜 License

Đây là bài tập học tập, chỉ dùng cho mục đích tham khảo và học tập.

---

**Lưu ý:** Các project trong repository này được phát triển nhằm mục đích học tập tại FPT University. Vui lòng không sao chép trực tiếp cho bài tập của bạn.
