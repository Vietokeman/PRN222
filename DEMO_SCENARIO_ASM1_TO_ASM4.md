# 🎯 KỊCH BẢN DEMO ASSIGNMENT 1-4
## PRN222 - Facility Feedback Helpdesk Request System

---

## 📋 MỤC LỤC
1. [ASM1 - Razor Pages Web App](#asm1---razor-pages-web-app)
2. [ASM2 - Worker Service](#asm2---worker-service)
3. [ASM3 - MVC Web App](#asm3---mvc-web-app)
4. [ASM4 - Blazor Web App](#asm4---blazor-web-app)
5. [Tính năng Sort Implementation](#tính-năng-sort-implementation)
6. [Validation Processing Code](#validation-processing-code)

---

## 🚀 ASM1 - RAZOR PAGES WEB APP

### 📌 **Tính năng chính:**
- ✅ Full CRUD Operations
- ✅ Login/Logout Authentication
- ✅ **SignalR Real-time Updates**
- ✅ **Search 3 fields** (Processing Action, Action Description, Type Name)
- ✅ **Sort cho tất cả columns**
- ✅ Pagination

### 🎬 **KỊCH BẢN DEMO:**

#### **1. LOGIN (3 phút)**
```
BƯỚC 1: Mở trình duyệt
- URL: https://localhost:7xxx (port của bạn)
- Redirect tự động về trang Login

BƯỚC 2: Đăng nhập
- Email: admin@fpt.edu.vn (hoặc account trong DB)
- Password: [password của bạn]
- Click "Login"
- ✅ Hiển thị thông báo "Login successful!"
- ✅ Redirect về trang Index với danh sách tickets

DEMO POINT: "Hệ thống sử dụng ASP.NET Core Identity để authentication"
```

#### **2. INDEX - VIEW LIST (2 phút)**
```
BƯỚC 1: Quan sát trang Index
- ✅ Header với nút "ADD NEW TICKET"
- ✅ Search Filters (3 fields)
- ✅ Bảng dữ liệu với các columns có icon sort
- ✅ Pagination controls

BƯỚC 2: Giải thích các columns
- Processing Code (PROC-xxxx-xxxx)
- Ticket Reference
- Action, Description
- Priority (Low/Medium/High với badges màu)
- Status (Active/Resolved/Closed)
- Auto-processing indicator
- Actions (View/Edit/Delete)

DEMO POINT: "Giao diện responsive, hiển thị đầy đủ thông tin ticket"
```

#### **3. SEARCH FUNCTIONALITY (3 phút)**
```
BƯỚC 1: Tìm kiếm theo Processing Action
- Nhập vào field "Processing Action": "Investigate"
- Click "Search"
- ✅ Kết quả lọc hiển thị chỉ các tickets có action chứa "Investigate"

BƯỚC 2: Tìm kiếm kết hợp
- Processing Action: "Fix"
- Action Description: "network"
- Click "Search"
- ✅ Kết quả lọc theo cả 2 điều kiện

BƯỚC 3: Tìm kiếm theo Type Name
- Type Name: "Hardware"
- Click "Search"
- ✅ Chỉ hiển thị tickets loại Hardware

BƯỚC 4: Clear search
- Click "Clear"
- ✅ Trở về hiển thị tất cả tickets

DEMO POINT: "Search hỗ trợ tìm kiếm theo 3 trường đồng thời, kết quả real-time"
```

#### **4. SORT FUNCTIONALITY (3 phút)**
```
BƯỚC 1: Sort theo Processing Code
- Click vào header "Code"
- ✅ Icon đổi thành mũi tên lên (↑) - Sort tăng dần
- ✅ Dữ liệu sắp xếp A-Z theo Code

BƯỚC 2: Sort ngược lại
- Click lại vào header "Code"
- ✅ Icon đổi thành mũi tên xuống (↓) - Sort giảm dần
- ✅ Dữ liệu sắp xếp Z-A

BƯỚC 3: Sort theo Priority Level
- Click vào header "Priority"
- ✅ Sắp xếp theo mức độ ưu tiên (Low → High hoặc ngược lại)

BƯỚC 4: Sort theo Created Date
- Click vào header "Created"
- ✅ Sắp xếp theo ngày tạo (cũ nhất → mới nhất)

DEMO POINT: "Sort hoạt động trên tất cả 14 columns, giữ nguyên filter và pagination"
```

#### **5. CREATE - THÊM MỚI TICKET (5 phút)**
```
BƯỚC 1: Click "ADD NEW TICKET"
- ✅ Chuyển sang trang Create với form đầy đủ fields

BƯỚC 2: Điền thông tin Basic Information
- Processing Code: "PROC-2024-0099" 
  ⚠️ Format: PROC-xxxx-xxxx (có validation)
  ⚠️ Thử nhập sai: "ABC123" → Lỗi validation hiển thị
- Ticket Reference: "TICK-2024-099"
- Processing Type: Chọn "Software Issue"
- Related Ticket Code: "PROC-2024-0050" (optional)

BƯỚC 3: Điền Processing Details
- Processing Action: "Investigate database connection timeout"
- Action Description: "Users report slow application performance"
- Priority Level: Chọn "High"
- Status: Chọn "Active"

BƯỚC 4: Điền Additional Information
- Overdue Days: 5
- Escalation Level: 2
- Is Auto Processed: Check ✓
- Processed By: "admin@fpt.edu.vn"
- Resolved Note: "Need to check connection pool settings"

BƯỚC 5: Submit
- Click "Create"
- ✅ Toast notification: "Ticket created successfully!"
- ✅ Redirect về trang Index
- ✅ Ticket mới hiển thị ở đầu danh sách

DEMO POINT: "Validation đầy đủ, Processing Code phải đúng format PROC-xxxx-xxxx"
```

#### **6. SIGNALR REAL-TIME DEMO (5 phút)** ⭐
```
CHUẨN BỊ:
- Mở 2 browser windows/tabs cùng lúc
- Browser 1: Đăng nhập user A
- Browser 2: Đăng nhập user B (hoặc cùng user)
- Cả 2 đều ở trang Index

BƯỚC 1: CREATE từ Browser 1
- Browser 1: Click "ADD NEW TICKET"
- Tạo ticket mới: "PROC-2024-0100"
- Click "Create"
- ✅ Browser 1: Toast "Ticket created successfully!"
- ⭐ Browser 2: KHÔNG REFRESH, ticket tự động xuất hiện!
- ⭐ Toast hiển thị: "Ticket 'PROC-2024-0100' created successfully via SignalR!"

BƯỚC 2: UPDATE từ Browser 2
- Browser 2: Click Edit ticket vừa tạo
- Sửa Status từ "Active" → "Resolved"
- Click "Save"
- ✅ Browser 2: Toast "Updated successfully!"
- ⭐ Browser 1: Row tự động update màu badge status!
- ⭐ Toast: "Ticket 'PROC-2024-0100' updated successfully via SignalR!"

BƯỚC 3: DELETE từ Browser 1
- Browser 1: Click Delete một ticket
- Confirm xóa
- ✅ Browser 1: Ticket biến mất
- ⭐ Browser 2: Ticket TỰ ĐỘNG BIẾN MẤT không cần refresh!
- ⭐ Toast: "Ticket deleted successfully via SignalR!"

DEMO POINT: 
"SignalR Hub đảm bảo real-time sync giữa multiple clients.
Console log hiển thị: Connection ID, ReceiverCreate, ReceiverUpdate, ReceiverDelete events"
```

#### **7. DETAILS - XEM CHI TIẾT (2 phút)**
```
BƯỚC 1: Click icon "👁️ View" của một ticket
- ✅ Trang Details hiển thị đầy đủ thông tin
- ✅ Các trường được nhóm theo sections
- ✅ Badges hiển thị Priority và Status với màu sắc

BƯỚC 2: Quay lại
- Click "Back to List"
- ✅ Quay về trang Index

DEMO POINT: "View chỉ đọc, không cho phép chỉnh sửa"
```

#### **8. EDIT - CẬP NHẬT (3 phút)**
```
BƯỚC 1: Click icon "✏️ Edit"
- ✅ Form Edit tương tự Create
- ✅ Các field đã điền sẵn dữ liệu hiện tại

BƯỚC 2: Sửa thông tin
- Đổi Priority Level: High → Medium
- Đổi Status: Active → Resolved
- Thêm Resolved Note: "Issue resolved by restarting service"

BƯỚC 3: Save
- Click "Update"
- ✅ Toast: "Updated successfully!"
- ✅ Redirect về Index
- ✅ Row được highlight với thông tin mới

DEMO POINT: "Edit giữ nguyên format validation của Processing Code"
```

#### **9. DELETE - XÓA (2 phút)**
```
BƯỚC 1: Click icon "🗑️ Delete"
- ✅ Trang Delete confirmation
- ✅ Hiển thị thông tin ticket cần xóa

BƯỚC 2: Confirm
- Click "Delete"
- ✅ Toast: "Deleted successfully!"
- ✅ Ticket biến mất khỏi danh sách

DEMO POINT: "Xóa có confirmation page để tránh xóa nhầm"
```

#### **10. PAGINATION (2 phút)**
```
BƯỚC 1: Thay đổi Page Size
- Dropdown "Show": Chọn "5"
- ✅ Chỉ hiển thị 5 records
- ✅ Số trang tăng lên

BƯỚC 2: Navigate pages
- Click page số 2
- ✅ Hiển thị 5 records tiếp theo
- Click "Next >"
- Click "Last >>"

BƯỚC 3: Pagination với Search + Sort
- Search: Type Name = "Hardware"
- Sort: Priority Level giảm dần
- ✅ Pagination vẫn hoạt động đúng với filtered data

DEMO POINT: "Pagination state được giữ nguyên khi search/sort"
```

#### **11. LOGOUT (1 phút)**
```
BƯỚC 1: Click username ở header
- Click "Logout"
- ✅ Redirect về trang Login
- ✅ Session cleared

BƯỚC 2: Thử truy cập Index trực tiếp
- Paste URL Index
- ✅ Tự động redirect về Login (Authorization required)

DEMO POINT: "Authorization middleware bảo vệ tất cả pages"
```

---

## ⚙️ ASM2 - WORKER SERVICE

### 📌 **Tính năng chính:**
- ✅ Background Service chạy theo interval
- ✅ Tự động xử lý tickets
- ✅ Logging hoạt động
- ✅ Cập nhật database

### 🎬 **KỊCH BẢN DEMO:**

#### **1. GIỚI THIỆU WORKER SERVICE (2 phút)**
```
KHÁI NIỆM:
- Worker Service là background service chạy độc lập
- Không có UI
- Thực hiện các tác vụ tự động theo lịch
- Chạy liên tục hoặc theo interval

CHỨC NĂNG TRONG PROJECT:
- Tự động xử lý tickets overdue
- Tự động escalate tickets theo priority
- Gửi email notifications (demo)
- Cập nhật trạng thái auto-processing

DEMO POINT: "Worker Service giống như một robot tự động làm việc ngầm 24/7"
```

#### **2. KHỞI ĐỘNG WORKER SERVICE (3 phút)**
```
BƯỚC 1: Mở Visual Studio
- Chọn project: FFHRRequestSystem.WorkerService
- Set as Startup Project
- Click Run (hoặc F5)

BƯỚC 2: Quan sát Console Output
✅ Console hiển thị:
[2024-12-15 10:00:00] Worker Service started
[2024-12-15 10:00:00] Processing tickets...
[2024-12-15 10:00:00] Found 5 active tickets
[2024-12-15 10:00:00] Processing ticket PROC-2024-0001
[2024-12-15 10:00:00] Ticket PROC-2024-0001: Overdue 10 days -> Escalating
[2024-12-15 10:00:00] Updated escalation level from 1 to 2
...

BƯỚC 3: Kiểm tra Logs
- Logs được ghi vào file: Logs/worker-yyyyMMdd.txt
- Mở file log
✅ Nội dung tương tự console với timestamp đầy đủ

DEMO POINT: "Service tự động khởi động và bắt đầu xử lý"
```

#### **3. DEMO CHỨC NĂNG TỰ ĐỘNG (5 phút)**
```
CHUẨN BỊ:
- Tạo 1 ticket có OverdueDays = 15 (quá hạn)
- Status = Active
- EscalationLevel = 1

BƯỚC 1: Chạy Worker Service
- Để service chạy trong 30 giây
✅ Console log:
[10:00:15] Found ticket PROC-2024-0088 overdue 15 days
[10:00:15] Auto-escalating ticket...
[10:00:15] Escalation level: 1 → 2
[10:00:15] IsAutoProcessed: false → true
[10:00:15] Database updated successfully

BƯỚC 2: Kiểm tra Database
- Mở SQL Server Management Studio
- Query: SELECT * FROM TicketProcessingVietN WHERE ProcessingCode = 'PROC-2024-0088'
✅ Kết quả:
  - EscalationLevel = 2 (đã tăng)
  - IsAutoProcessed = true (đã được đánh dấu)
  - ModifiedDate = [thời gian vừa rồi]

BƯỚC 3: Kiểm tra từ Web App
- Mở ASM1 hoặc ASM3
- Tìm ticket PROC-2024-0088
✅ Badge "Auto" hiển thị
✅ EscalationLevel hiển thị 2

DEMO POINT: "Worker Service tự động phát hiện và xử lý tickets không cần can thiệp"
```

#### **4. DEMO LOGGING & MONITORING (3 phút)**
```
BƯỚC 1: Xem Real-time Logs
✅ Console hiển thị từng bước:
[10:01:00] ========== Processing Cycle Started ==========
[10:01:00] Database connection established
[10:01:00] Retrieved 125 active tickets
[10:01:00] Filtering overdue tickets...
[10:01:00] Found 12 overdue tickets
[10:01:05] Processing ticket 1/12: PROC-2024-0010
[10:01:05] - Current overdue: 8 days
[10:01:05] - Current escalation: 1
[10:01:05] - Action: Escalate to level 2
[10:01:05] - Status: Updated successfully
[10:01:06] Processing ticket 2/12: PROC-2024-0025
...
[10:01:30] ========== Cycle Completed ==========
[10:01:30] Total processed: 12 tickets
[10:01:30] Successful: 12
[10:01:30] Failed: 0
[10:01:30] Next run in 300 seconds

BƯỚC 2: Kiểm tra Log File
- Mở: Logs/worker-20241215.txt
✅ Chi tiết hơn console:
  - Stack traces nếu có lỗi
  - Performance metrics
  - Database query details

DEMO POINT: "Logging đầy đủ giúp tracking và debugging"
```

#### **5. DEMO ERROR HANDLING (2 phút)**
```
BƯỚC 1: Simulate lỗi database
- Stop SQL Server tạm thời
- Để Worker Service chạy

✅ Console hiển thị:
[10:05:00] ERROR: Failed to connect to database
[10:05:00] System.Data.SqlClient.SqlException: Cannot open database
[10:05:00] Retrying in 60 seconds...
[10:05:00] Will attempt 3 more times before stopping

BƯỚC 2: Khôi phục database
- Start SQL Server lại

✅ Console:
[10:06:00] Database connection restored
[10:06:00] Resuming normal operation

DEMO POINT: "Service có error handling và retry logic"
```

#### **6. CONFIGURATION (2 phút)**
```
MỞ FILE: appsettings.json

{
  "WorkerSettings": {
    "IntervalSeconds": 300,        // Chạy mỗi 5 phút
    "OverdueThreshold": 7,         // Quá hạn > 7 ngày
    "AutoEscalateEnabled": true,   // Bật tự động escalate
    "MaxEscalationLevel": 5,       // Tối đa level 5
    "EmailNotifications": false    // Tắt email (demo)
  }
}

DEMO THAY ĐỔI CONFIG:
- Đổi IntervalSeconds: 300 → 60 (chạy mỗi 1 phút)
- Save file
- Restart Worker Service
✅ Service chạy với interval mới

DEMO POINT: "Cấu hình linh hoạt không cần compile lại code"
```

#### **7. STOP WORKER SERVICE (1 phút)**
```
BƯỚC 1: Graceful shutdown
- Nhấn Ctrl+C trong console
✅ Console:
[10:10:00] Shutdown signal received
[10:10:00] Finishing current cycle...
[10:10:05] Saving state...
[10:10:05] Worker Service stopped gracefully

DEMO POINT: "Service shutdown an toàn, không mất dữ liệu đang xử lý"
```

---

## 🌐 ASM3 - MVC WEB APP

### 📌 **Tính năng chính:**
- ✅ Full CRUD Operations (MVC Pattern)
- ✅ Login/Logout Authentication
- ✅ **Search 3 fields**
- ✅ **Sort cho tất cả columns**
- ✅ Pagination
- ✅ **SignalR Real-time Updates**

### 🎬 **KỊCH BẢN DEMO:**

#### **1. GIỚI THIỆU MVC PATTERN (2 phút)**
```
GIẢI THÍCH KIẾN TRÚC:
📁 Controllers/
  - TicketProcessingVietNsController.cs
  - AccountController.cs
  → Xử lý request, business logic

📁 Views/
  - TicketProcessingVietNs/
    - Index.cshtml
    - Create.cshtml
    - Edit.cshtml
  → Giao diện người dùng

📁 Models/
  - TicketProcessingVietN.cs
  → Dữ liệu entities

DEMO POINT: "MVC tách biệt rõ ràng giữa logic, data và presentation"
```

#### **2. LOGIN (2 phút)**
```
BƯỚC 1: Khởi động project
- URL: https://localhost:5xxx
- Auto redirect → /Account/Login

BƯỚC 2: Đăng nhập
- Email: admin@fpt.edu.vn
- Password: [your password]
- Click "Login"
✅ Redirect → /TicketProcessingVietNs/Index
✅ Navbar hiển thị username

DEMO POINT: "Authentication sử dụng ASP.NET Core Identity"
```

#### **3. INDEX - MVC ROUTING (2 phút)**
```
QUAN SÁT URL PATTERN:
- Create: /TicketProcessingVietNs/Create
- Edit: /TicketProcessingVietNs/Edit/[id]
- Details: /TicketProcessingVietNs/Details/[id]
- Delete: /TicketProcessingVietNs/Delete/[id]

GIẢI THÍCH ROUTING:
Controller: TicketProcessingVietNs
Action: Index, Create, Edit, Details, Delete
Parameter: id (Guid)

DEMO POINT: "URL pattern rõ ràng theo convention của MVC"
```

#### **4. SEARCH 3 FIELDS (3 phút)**
```
BƯỚC 1: Mở Search Filters section
✅ 3 input fields:
  - Processing Action
  - Action Description
  - Type Name

BƯỚC 2: Search đơn giản
- Processing Action: "Repair"
- Click "Search"
✅ URL thay đổi: ?processingAction=Repair
✅ Chỉ hiển thị tickets có action "Repair"

BƯỚC 3: Search kết hợp
- Processing Action: "Fix"
- Action Description: "server"
- Type Name: "Infrastructure"
- Click "Search"
✅ URL: ?processingAction=Fix&actionDescription=server&typeName=Infrastructure
✅ Kết quả filtered theo cả 3 điều kiện

BƯỚC 4: Clear filters
- Click "Clear"
✅ URL reset về /Index
✅ Hiển thị tất cả records

DEMO POINT: "Search params được bind vào URL, có thể bookmark hoặc share"
```

#### **5. SORT COLUMNS (3 phút)**
```
BƯỚC 1: Kiểm tra headers
✅ Tất cả columns có icon sort mờ (↓)
✅ Hover hiển thị "Click to sort"

BƯỚC 2: Sort theo Code
- Click "Code"
✅ URL: ?sortColumn=ProcessingCode&sortDirection=asc
✅ Icon: ↑ (mũi tên lên)
✅ Data sắp xếp A→Z

BƯỚC 3: Reverse sort
- Click "Code" lần nữa
✅ URL: ?sortColumn=ProcessingCode&sortDirection=desc
✅ Icon: ↓ (mũi tên xuống)
✅ Data sắp xếp Z→A

BƯỚC 4: Sort với Search
- Search: Type Name = "Software"
- Sort: Priority Level (descending)
✅ URL: ?typeName=Software&sortColumn=PriorityLevel&sortDirection=desc
✅ Kết quả: Filtered + Sorted

DEMO POINT: "Sort state được preserve trong URL, hoạt động với search và pagination"
```

#### **6. CREATE - MVC POST (4 phút)**
```
BƯỚC 1: Click "ADD NEW TICKET"
✅ GET /TicketProcessingVietNs/Create
✅ Form hiển thị với dropdowns populated

BƯỚC 2: Fill form
- Processing Code: "PROC-2024-0150"
  ⚠️ Test validation: Nhập "ABC" → Browser hiển thị lỗi
  ⚠️ Format đúng: PROC-xxxx-xxxx
- Ticket Reference: "TICK-150"
- Processing Type: "Network Issue"
- Processing Action: "Fix router configuration"
- Priority: "High"
- Status: "Active"

BƯỚC 3: Submit
- Click "Create"
✅ POST /TicketProcessingVietNs/Create
✅ Server-side validation
✅ TempData message: "Ticket created successfully!"
✅ Redirect → /Index

BƯỚC 4: Verify
✅ Ticket mới hiển thị trong table
✅ Toast notification (nếu có SignalR)

DEMO POINT: "MVC POST-Redirect-GET pattern, validation cả client và server"
```

#### **7. DETAILS - VIEW MODEL (2 phút)**
```
BƯỚC 1: Click "View" icon
✅ GET /TicketProcessingVietNs/Details/[guid]

BƯỚC 2: Quan sát View
✅ Read-only display
✅ Sections: Basic Info, Processing Details, Additional Info
✅ Badges hiển thị Priority và Status

BƯỚC 3: Check View Source (F12)
✅ HTML semantics: <dl>, <dt>, <dd>
✅ Bootstrap classes: card, badge, etc.

DEMO POINT: "Details view dùng Display templates, không có input fields"
```

#### **8. EDIT - MODEL BINDING (3 phút)**
```
BƯỚC 1: Click "Edit" icon
✅ GET /TicketProcessingVietNs/Edit/[guid]
✅ Form populated với current data

BƯỚC 2: Modify data
- Status: Active → Resolved
- Resolved Note: "Router config updated successfully"
- Processed By: "admin@fpt.edu.vn"

BƯỚC 3: Submit
- Click "Update"
✅ POST /TicketProcessingVietNs/Edit
✅ Model binding tự động map form data
✅ Validation
✅ TempData: "Updated successfully!"
✅ Redirect → Index

BƯỚC 4: Verify update
✅ Row updated với badge "Resolved"
✅ SignalR broadcast update (nếu có)

DEMO POINT: "MVC model binding tự động, validation attributes từ model"
```

#### **9. DELETE - CONFIRMATION PATTERN (2 phút)**
```
BƯỚC 1: Click "Delete" icon
✅ GET /TicketProcessingVietNs/Delete/[guid]
✅ Confirmation page với thông tin ticket

BƯỚC 2: Review thông tin
✅ All fields displayed read-only
✅ Warning: "Are you sure you want to delete this?"

BƯỚC 3: Confirm
- Click "Delete" button
✅ POST /TicketProcessingVietNs/Delete/[guid]
✅ Database delete
✅ TempData: "Deleted successfully!"
✅ Redirect → Index

DEMO POINT: "Safe delete pattern với confirmation step"
```

#### **10. PAGINATION (2 phút)**
```
BƯỚC 1: Page size selector
- Chọn "10" items per page
✅ URL: ?pageSize=10&currentPage=1

BƯỚC 2: Navigate
- Click "Next >"
✅ URL: ?pageSize=10&currentPage=2
- Click page số "3"
✅ URL: ?pageSize=10&currentPage=3

BƯỚC 3: Pagination info
✅ Hiển thị: "Showing 21 to 30 of 125 entries"

BƯỚC 4: Complex scenario
- Search: typeName=Hardware
- Sort: Priority desc
- Page: 2, Size: 25
✅ URL: ?typeName=Hardware&sortColumn=PriorityLevel&sortDirection=desc&pageSize=25&currentPage=2
✅ All states preserved

DEMO POINT: "ViewData truyền pagination state từ Controller → View"
```

#### **11. SIGNALR (Optional - nếu implement) (3 phút)**
```
(Tương tự ASM1 nếu có implement SignalR trong ASM3)
- Mở 2 browsers
- Create từ browser 1 → Hiển thị tự động ở browser 2
- Update từ browser 2 → Real-time update ở browser 1
```

#### **12. LOGOUT (1 phút)**
```
BƯỚC 1: Click "Logout"
✅ POST /Account/Logout
✅ Session cleared
✅ Redirect → /Account/Login

BƯỚC 2: Test authorization
- Paste URL: /TicketProcessingVietNs/Index
✅ Redirect → /Account/Login?ReturnUrl=/TicketProcessingVietNs/Index

DEMO POINT: "Authorization filter bảo vệ tất cả actions"
```

---

## ⚡ ASM4 - BLAZOR WEB APP

### 📌 **Tính năng chính:**
- ✅ Full CRUD Operations (Blazor Components)
- ✅ Login/Logout Authentication
- ✅ **Search 3 fields**
- ✅ **Sort cho tất cả columns**
- ✅ **Client-side pagination**
- ✅ **Interactive UI - No page reload**

### 🎬 **KỊCH BẢN DEMO:**

#### **1. GIỚI THIỆU BLAZOR (2 phút)**
```
BLAZOR ĐẶC ĐIỂM:
✅ Component-based architecture
✅ C# code chạy trên browser (WebAssembly) hoặc server
✅ No JavaScript framework (React/Vue/Angular)
✅ Real-time UI updates
✅ No page reload

KIẾN TRÚC:
📁 Components/Pages/TicketProcessingVietNPages/
  - Index.razor
  - Create.razor
  - Edit.razor
  - Delete.razor
  - Details.razor

DEMO POINT: "Blazor = SPA với C#, không cần JavaScript"
```

#### **2. LOGIN (2 phút)**
```
BƯỚC 1: Khởi động app
- URL: https://localhost:5xxx
✅ Blazor app initialization
✅ Redirect → /Account/Login

BƯỚC 2: Login
- Email: admin@fpt.edu.vn
- Password: [password]
- Click "Login"
✅ NO PAGE RELOAD
✅ Smooth transition → /ticketprocessingvietns

DEMO POINT: "Blazor navigation không reload page"
```

#### **3. INDEX - BLAZOR RENDERING (3 phút)**
```
BƯỚC 1: Quan sát initial load
✅ "Loading..." message hiển thị ngắn
✅ Data được fetch từ service
✅ Table render với full data

BƯỚC 2: Mở F12 Developer Tools → Network tab
✅ Không có page reload khi tương tác
✅ Chỉ có API calls (nếu dùng InteractiveWebAssembly)

BƯỚC 3: Inspect component
- Right-click → Inspect
✅ Blazor binding attributes: blazor-component-id
✅ Event handlers: onclick, oninput

DEMO POINT: "Blazor render trực tiếp trên client, không có postback"
```

#### **4. SEARCH - REACTIVE BINDING (4 phút)**
```
BƯỚC 1: Real-time search (nếu dùng @bind:event="oninput")
- Type vào "Processing Action": "F"
✅ NO BUTTON CLICK
✅ Table tự động filter
- Type tiếp: "Fi"
✅ Results update ngay lập tức
- Type tiếp: "Fix"
✅ Final filtered results

BƯỚC 2: Multiple field search
- Processing Action: "Fix"
- Action Description: "network"
- Type Name: "Infrastructure"
✅ All 3 @bind bindings hoạt động
✅ Click "Search" button
✅ Results filtered tức thì

BƯỚC 3: Clear search
- Click "Clear"
✅ All input fields cleared
✅ Table reset về full data
✅ Không có page reload

BƯỚC 4: Check @code
```csharp
@code {
    private string searchProcessingAction = "";
    private string searchActionDescription = "";
    private string searchTypeName = "";
    
    private async Task PerformSearch()
    {
        allTickets = await _ticketService.SearchAsync(
            searchProcessingAction, 
            searchActionDescription, 
            searchTypeName
        );
        currentPage = 1;
        totalItems = allTickets.Count;
        UpdatePagedTickets();
        StateHasChanged(); // Force UI update
    }
}
```

DEMO POINT: "Blazor two-way binding (@bind), reactive UI updates"
```

#### **5. SORT - BLAZOR EVENTS (4 phút)**
```
BƯỚC 1: Click sort header
- Click "Code"
✅ @onclick="() => SortColumn("ProcessingCode")" triggered
✅ Icon changes: ↑
✅ Table re-renders sorted
✅ NO PAGE RELOAD

BƯỚC 2: Reverse sort
- Click "Code" again
✅ Icon: ↓
✅ Sort direction reversed
✅ Instant UI update

BƯỚC 3: Check @code logic
```csharp
@code {
    private string sortColumn = "CreatedDate";
    private string sortDirection = "desc";
    
    private void SortColumn(string columnName)
    {
        if (sortColumn == columnName)
        {
            sortDirection = sortDirection == "asc" ? "desc" : "asc";
        }
        else
        {
            sortColumn = columnName;
            sortDirection = "asc";
        }
        
        ApplySorting();
        UpdatePagedTickets();
        StateHasChanged();
    }
}
```

BƯỚC 4: Performance check
- Sort large dataset (100+ items)
✅ Instant response
✅ No network call (client-side sort)

DEMO POINT: "Blazor event handling trong C#, không cần JavaScript"
```

#### **6. CREATE - BLAZOR FORMS (5 phút)**
```
BƯỚC 1: Click "ADD NEW TICKET"
✅ Blazor navigation → /ticketprocessingvietns/create
✅ NO page reload

BƯỚC 2: Observe EditForm
```razor
<EditForm Model="TicketProcessingVietN" OnValidSubmit="AddTicketProcessingVietN">
    <DataAnnotationsValidator />
    <ValidationSummary class="text-danger" />
    
    <InputText @bind-Value="TicketProcessingVietN.ProcessingCode" 
               pattern="PROC-\d{4}-\d{4}" />
    <ValidationMessage For="() => TicketProcessingVietN.ProcessingCode" />
</EditForm>
```

BƯỚC 3: Test validation
- Processing Code: "ABC" (sai format)
- Blur khỏi field
✅ Validation message: "Format: PROC-xxxx-xxxx"
✅ Border đổi màu đỏ
✅ NO submit được

BƯỚC 4: Fill valid data
- Processing Code: "PROC-2024-0200"
- Ticket Reference: "TICK-200"
- Select Processing Type dropdown
✅ Dropdown populated từ service
- Fill remaining fields

BƯỚC 5: Submit
- Click "Create"
✅ OnValidSubmit triggered
```csharp
@code {
    private async Task AddTicketProcessingVietN()
    {
        TicketProcessingVietN.TicketProcessingVietNid = Guid.NewGuid();
        TicketProcessingVietN.CreatedDate = DateTime.Now;
        
        await _ticketProcessingService.CreateAsync(TicketProcessingVietN);
        
        NavigationManager.NavigateTo("/ticketprocessingvietns");
    }
}
```
✅ Toast notification (nếu có)
✅ Navigate về Index
✅ New record hiển thị

DEMO POINT: "EditForm tích hợp validation, InputText/InputSelect components"
```

#### **7. DETAILS - BLAZOR PARAMETERS (2 phút)**
```
BƯỚC 1: Click "View" icon
✅ URL: /ticketprocessingvietns/details?id=[guid]

BƯỚC 2: Component receives parameter
```razor
@code {
    [Parameter]
    [SupplyParameterFromQuery]
    public Guid Id { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        ticketProcessing = await _service.GetByIdAsync(Id);
    }
}
```

BƯỚC 3: Observe render
✅ Data loaded asynchronously
✅ Read-only display
✅ Badges và formatting

DEMO POINT: "Blazor parameters từ URL, lifecycle methods"
```

#### **8. EDIT - TWO-WAY BINDING (3 phút)**
```
BƯỚC 1: Click "Edit"
✅ Navigate → /ticketprocessingvietns/edit?id=[guid]

BƯỚC 2: Form pre-populated
✅ @bind-Value binds model properties
✅ Dropdowns show current selection

BƯỚC 3: Make changes real-time
- Change Priority: High → Low
✅ Model updated instantly (no blur needed với @bind)
- Change Status: Active → Resolved
✅ Binding updates model

BƯỚC 4: Submit
- Click "Update"
```csharp
@code {
    private async Task UpdateTicketProcessingVietN()
    {
        TicketProcessingVietN.ModifiedDate = DateTime.Now;
        await _service.UpdateAsync(TicketProcessingVietN);
        NavigationManager.NavigateTo("/ticketprocessingvietns");
    }
}
```
✅ Navigate về Index
✅ Row updated

DEMO POINT: "Two-way binding với @bind-Value, automatic model updates"
```

#### **9. DELETE - BLAZOR CONFIRM (2 phút)**
```
BƯỚC 1: Click "Delete"
✅ Navigate → /ticketprocessingvietns/delete?id=[guid]

BƯỚC 2: Confirmation display
✅ Ticket details shown
✅ Warning message

BƯỚC 3: Confirm delete
- Click "Delete" button
```csharp
@code {
    private async Task DeleteTicket()
    {
        await _service.DeleteAsync(Id);
        NavigationManager.NavigateTo("/ticketprocessingvietns");
    }
}
```
✅ Record deleted
✅ Navigate back

DEMO POINT: "Blazor NavigationManager for routing"
```

#### **10. PAGINATION - CLIENT-SIDE (3 phút)**
```
BƯỚC 1: Observe pagination logic
```csharp
@code {
    private int currentPage = 1;
    private int pageSize = 10;
    private int totalItems = 0;
    private List<TicketProcessingVietN> pagedTickets = new();
    
    private int TotalPages => (int)Math.Ceiling((double)totalItems / pageSize);
    
    private void UpdatePagedTickets()
    {
        pagedTickets = allTickets
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    
    private void ChangePage(int newPage)
    {
        if (newPage >= 1 && newPage <= TotalPages)
        {
            currentPage = newPage;
            UpdatePagedTickets();
            StateHasChanged();
        }
    }
}
```

BƯỚC 2: Test pagination
- Click page "2"
✅ Instant transition (no API call)
✅ Table updates với records 11-20

BƯỚC 3: Change page size
- Select "25" từ dropdown
```razor
<select @onchange="HandlePageSizeChange">
    @foreach (var size in PageSizeOptions)
    {
        <option value="@size">@size</option>
    }
</select>
```
✅ Page resets về 1
✅ 25 items hiển thị

BƯỚC 4: Pagination với search + sort
- Search filtered: 50 results
- Sort: Priority desc
- Page size: 10
- Go to page 3
✅ All states preserved
✅ Correct 21-30 records shown

DEMO POINT: "Client-side pagination = instant, no server round-trip"
```

#### **11. PERFORMANCE CHECK (2 phút)**
```
BƯỚC 1: Network tab inspection
- F12 → Network tab
- Perform various actions
✅ Initial load: Download Blazor runtime
✅ Data fetch: API calls to service
✅ Navigation: NO additional page loads
✅ Interactions: NO HTTP requests

BƯỚC 2: Memory usage
- F12 → Performance/Memory tab
- Record session
- Perform actions: search, sort, navigate
✅ Memory stable (no leaks)
✅ Rendering fast

DEMO POINT: "Blazor SPA performance như React/Vue nhưng bằng C#"
```

#### **12. LOGOUT (1 phút)**
```
BƯỚC 1: Click "Logout"
✅ Navigate → /Account/Logout
✅ Session cleared
✅ Redirect → /Account/Login

BƯỚC 2: Test protected routes
- Type URL: /ticketprocessingvietns
✅ AuthorizeView component
✅ Redirect to login

DEMO POINT: "Blazor authorization với AuthorizeView"
```

---

## 📊 TÍNH NĂNG SORT IMPLEMENTATION

### **So sánh 3 Project:**

| Feature | ASM1 (Razor Pages) | ASM3 (MVC) | ASM4 (Blazor) |
|---------|-------------------|------------|---------------|
| **Sort Method** | Server-side | Server-side | Client-side |
| **State Management** | Query string parameters | Query string parameters | Component state variables |
| **Icon Display** | Razor conditional | Razor conditional | Blazor conditional |
| **Event Handling** | `asp-route-*` tag helpers | `asp-route-*` tag helpers | `@onclick` events |
| **Data Sorting** | LINQ in PageModel | LINQ in Controller | LINQ in @code |
| **Page Reload** | Yes (full page) | Yes (full page) | No (SPA) |

### **ASM1 Implementation:**
```csharp
// Index.cshtml.cs
[BindProperty(SupportsGet = true)]
public string SortColumn { get; set; } = "CreatedDate";

[BindProperty(SupportsGet = true)]
public string SortDirection { get; set; } = "desc";

private IList<TicketProcessingVietN> ApplySorting(IList<TicketProcessingVietN> items)
{
    var query = items.AsQueryable();
    var isAscending = SortDirection?.ToLower() == "asc";
    
    query = SortColumn switch
    {
        "ProcessingCode" => isAscending 
            ? query.OrderBy(x => x.ProcessingCode) 
            : query.OrderByDescending(x => x.ProcessingCode),
        "PriorityLevel" => isAscending 
            ? query.OrderBy(x => x.PriorityLevel) 
            : query.OrderByDescending(x => x.PriorityLevel),
        // ... các columns khác
        _ => query.OrderByDescending(x => x.CreatedDate)
    };
    
    return query.ToList();
}
```

```razor
<!-- Index.cshtml -->
<th>
    <a asp-page="./Index" 
       asp-route-sortColumn="ProcessingCode" 
       asp-route-sortDirection="@(Model.SortColumn == "ProcessingCode" && Model.SortDirection == "asc" ? "desc" : "asc")"
       asp-route-processingAction="@Model.ProcessingAction"
       asp-route-actionDescription="@Model.ActionDescription"
       asp-route-typeName="@Model.TypeName"
       asp-route-pageSize="@Model.PageSize"
       asp-route-currentPage="@Model.CurrentPage">
        Code
        @if (Model.SortColumn == "ProcessingCode")
        {
            <i class="bi bi-caret-@(Model.SortDirection == "asc" ? "up" : "down")-fill"></i>
        }
        else
        {
            <i class="bi bi-caret-down" style="opacity: 0.3;"></i>
        }
    </a>
</th>
```

### **ASM3 Implementation:**
```csharp
// TicketProcessingVietNsController.cs
public async Task<IActionResult> Index(
    string processingAction, 
    string actionDescription, 
    string typeName, 
    int currentPage = 1, 
    int pageSize = 10, 
    string sortColumn = "CreatedDate", 
    string sortDirection = "desc")
{
    // ... get data
    
    ViewData["SortColumn"] = sortColumn;
    ViewData["SortDirection"] = sortDirection;
    
    // Apply sorting
    items = ApplySorting(items, sortColumn, sortDirection);
    
    // ... pagination
    
    return View(pagedItems);
}

private List<TicketProcessingVietN> ApplySorting(
    List<TicketProcessingVietN> items, 
    string sortColumn, 
    string sortDirection)
{
    var query = items.AsQueryable();
    var isAscending = sortDirection?.ToLower() == "asc";
    
    // Similar switch logic as ASM1
    
    return query.ToList();
}
```

```razor
<!-- Index.cshtml -->
<th>
    <a asp-action="Index" 
       asp-route-sortColumn="ProcessingCode" 
       asp-route-sortDirection="@(ViewData["SortColumn"]?.ToString() == "ProcessingCode" && ViewData["SortDirection"]?.ToString() == "asc" ? "desc" : "asc")"
       asp-route-processingAction="@ViewData["ProcessingAction"]"
       asp-route-actionDescription="@ViewData["ActionDescription"]"
       asp-route-typeName="@ViewData["TypeName"]"
       asp-route-pageSize="@ViewData["PageSize"]"
       asp-route-currentPage="@ViewData["CurrentPage"]">
        Code
        @if (ViewData["SortColumn"]?.ToString() == "ProcessingCode")
        {
            <i class="bi bi-caret-@(ViewData["SortDirection"]?.ToString() == "asc" ? "up" : "down")-fill"></i>
        }
        else
        {
            <i class="bi bi-caret-down" style="opacity: 0.3;"></i>
        }
    </a>
</th>
```

### **ASM4 Implementation:**
```csharp
// Index.razor @code
@code {
    private string sortColumn = "CreatedDate";
    private string sortDirection = "desc";
    
    private void SortColumn(string columnName)
    {
        if (sortColumn == columnName)
        {
            sortDirection = sortDirection == "asc" ? "desc" : "asc";
        }
        else
        {
            sortColumn = columnName;
            sortDirection = "asc";
        }
        
        currentPage = 1;
        ApplySorting();
        UpdatePagedTickets();
        StateHasChanged();
    }
    
    private void ApplySorting()
    {
        var isAscending = sortDirection == "asc";
        
        allTickets = sortColumn switch
        {
            "ProcessingCode" => isAscending 
                ? allTickets.OrderBy(x => x.ProcessingCode).ToList() 
                : allTickets.OrderByDescending(x => x.ProcessingCode).ToList(),
            "PriorityLevel" => isAscending 
                ? allTickets.OrderBy(x => x.PriorityLevel).ToList() 
                : allTickets.OrderByDescending(x => x.PriorityLevel).ToList(),
            // ... các columns khác
            _ => allTickets.OrderByDescending(x => x.CreatedDate).ToList()
        };
    }
}
```

```razor
<!-- Index.razor -->
<th style="cursor: pointer;" @onclick='() => SortColumn("ProcessingCode")'>
    <div style="display: flex; align-items: center; justify-content: space-between;">
        Code
        @if (sortColumn == "ProcessingCode")
        {
            <i class="bi bi-caret-@(sortDirection == "asc" ? "up" : "down")-fill"></i>
        }
        else
        {
            <i class="bi bi-caret-down" style="opacity: 0.3;"></i>
        }
    </div>
</th>
```

---

## ✅ VALIDATION PROCESSING CODE

### **Format:** `PROC-\d{4}-\d{4}`

### **Examples:**
- ✅ Valid: `PROC-2024-0001`
- ✅ Valid: `PROC-2024-9999`
- ✅ Valid: `PROC-1999-0123`
- ❌ Invalid: `PROC-24-01` (thiếu số)
- ❌ Invalid: `proc-2024-0001` (lowercase)
- ❌ Invalid: `PROC-2024-001` (thiếu 1 số)
- ❌ Invalid: `ABC-2024-0001` (sai prefix)

### **Implementation in all ASM:**

**HTML5 Pattern Validation:**
```html
<input type="text" 
       pattern="PROC-\d{4}-\d{4}" 
       title="Format: PROC-xxxx-xxxx (e.g., PROC-2024-0003)"
       placeholder="PROC-2024-0003"
       required />
<small class="form-text text-muted">
    Format: PROC-xxxx-xxxx (e.g., PROC-2024-0003)
</small>
```

**Browser Behavior:**
- User nhập sai format → Submit bị block
- Tooltip hiển thị: "Format: PROC-xxxx-xxxx (e.g., PROC-2024-0003)"
- Border input đổi màu đỏ khi invalid

**Locations:**
- ✅ ASM1: Create.cshtml, Edit.cshtml
- ✅ ASM3: Create.cshtml, Edit.cshtml
- ✅ ASM4: Create.razor, Edit.razor

---

## 🎓 TIPS DEMO HIỆU QUẢ

### **Chuẩn bị trước:**
1. ✅ Database có sẵn data mẫu đa dạng
2. ✅ Test tất cả features trước khi demo
3. ✅ Chuẩn bị 2 browser/tabs cho SignalR demo
4. ✅ Bookmark các URLs quan trọng
5. ✅ Clear browser cache/cookies

### **Trong lúc demo:**
1. ✅ Giải thích TRƯỚC KHI thực hiện
2. ✅ Di chuyển chuột CHẬM để audience theo dõi
3. ✅ Pause sau mỗi action để quan sát kết quả
4. ✅ Mở F12 Developer Tools khi cần
5. ✅ Highlight các key features

### **Xử lý lỗi:**
1. ✅ Nếu lỗi xảy ra, KHÔNG PANIC
2. ✅ Giải thích lỗi đó là gì
3. ✅ Có backup plan (screenshots/videos)
4. ✅ Restart service/browser nếu cần

### **Thời gian phân bổ (45 phút total):**
- ASM1: 12 phút
- ASM2: 8 phút
- ASM3: 10 phút
- ASM4: 12 phút
- Q&A: 3 phút

---

## 📝 CHECKLIST TRƯỚC KHI DEMO

### **Technical:**
- [ ] All projects build successfully
- [ ] Database seeded với data mẫu
- [ ] Connection strings correct
- [ ] All services running
- [ ] Browser cookies cleared

### **Demo Data:**
- [ ] Có tickets với các status khác nhau
- [ ] Có tickets overdue để demo Worker Service
- [ ] Có tickets với priorities khác nhau
- [ ] Có đủ data để demo pagination (>50 records)

### **Environment:**
- [ ] Visual Studio opened với all projects
- [ ] SQL Server Management Studio ready
- [ ] 2 browsers prepared cho SignalR
- [ ] PowerPoint/slides backup (nếu có)
- [ ] Screen recording software (backup)

---

## 🏆 ĐIỂM MẠNH CẦN HIGHLIGHT

### **ASM1:**
- ⭐ SignalR real-time sync
- ⭐ Clean Razor Pages architecture
- ⭐ Full validation

### **ASM2:**
- ⭐ Background processing
- ⭐ Automated workflows
- ⭐ Comprehensive logging

### **ASM3:**
- ⭐ MVC pattern rõ ràng
- ⭐ URL routing RESTful
- ⭐ ViewData/TempData usage

### **ASM4:**
- ⭐ Modern SPA experience
- ⭐ Client-side performance
- ⭐ Component reusability
- ⭐ No page reload

### **All ASM:**
- ✅ Consistent UI/UX
- ✅ Sort + Search + Pagination
- ✅ Processing Code validation
- ✅ Authentication/Authorization
- ✅ Bootstrap responsive design

---

**Good luck with your demo! 🚀**
