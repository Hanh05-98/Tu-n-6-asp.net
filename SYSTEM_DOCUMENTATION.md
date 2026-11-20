# Week 6 MVC

Trạng thái: Controller/View của Product, Genre và Movie đã có; database của project cần được tạo/cập nhật riêng trước khi chạy CRUD local.
Target framework: `net10.0`. Database provider: SQLite, cấu hình trong `Program.cs` và `appsettings.json`.

## Scaffold Genre và Movie

Hai lệnh ban đầu thiếu provider, nên công cụ mặc định chọn SQL Server và dừng vì không có package SqlServer. Dùng các lệnh sau tại thư mục chứa `week6.csproj`:

```bash
dotnet aspnet-codegenerator controller -name GenresController -m Genre -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider sqlite
dotnet aspnet-codegenerator controller -name MoviesController -m Movie -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider sqlite
```

Không chạy lại với `--force` khi đã sửa Controller/View. Microsoft template tạo Index, Details, Create, Edit, Delete, kiểm tra ModelState và chống giả mạo request cho POST. Movies dùng dropdown thể loại theo Genre.Name.

Tham khảo: [Microsoft code generator](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-10.0).

## Model và dữ liệu

- Genre.Name là string, bắt buộc và dài 3–100 ký tự.
- Movie.Title bắt buộc, dài 3–200 ký tự; ReleaseDate bắt buộc; Duration từ 1–600 phút.
- Range từ 1 đến int.MaxValue nằm trên GenreId. Navigation Genre dùng ValidateNever vì form gửi GenreId; EF nạp đối tượng liên quan. Khóa ngoại được EF cấu hình là bắt buộc, dùng cascade delete mặc định khi xóa Genre có Movie liên quan. Range không thay thế kiểm tra khóa ngoại của database.
- AppDbContext giữ Product và thêm DbSet cho Genre, Movie.
- Không chạy migration hoặc tạo schema dữ liệu trong project. SQLite có thể tạo file app.db rỗng khi công cụ/ứng dụng mở kết nối; database local được bỏ qua bởi Git và giữ nguyên khi ứng dụng đang chạy. Database dùng để kiểm chứng nằm ngoài project và được dọn sau khi kiểm tra. Cần CLI EF tương thích .NET/EF Core 10 trước khi anh tạo migration và cập nhật database.

## Chạy và Git

Sau khi chuẩn bị database, chạy `dotnet run --launch-profile https`. Các route là `/Products`, `/Genres`, `/Movies`; cổng lấy từ launchSettings và log chạy app.

Repo: https://github.com/thuowgn05/week-06.git, nhánh main. `.gitignore` chuẩn SDK loại bin/obj và các file công cụ; thêm quy tắc bỏ qua app.db, sidecar SQLite, .env và AGENTS.md local. Giữ lịch sử source và static assets, không commit database thử hay credential.

## Kiểm chứng và giới hạn

Đạt 17 trường hợp validation và kiểm tra metadata khóa ngoại bằng EF trên database tạm. Đạt 8/8 kịch bản trình duyệt: hai vòng, mỗi vòng desktop 1440px/mobile 390px và light/dark; kiểm tra Create/Details/Edit/Delete/Index của Genre/Movie, dropdown, lỗi server, Home/Privacy và trang đọc/form Product. Không có page overflow hoặc lỗi JavaScript trong các kịch bản đó. Build thành công, 0 lỗi, 2 cảnh báo NU1901. Kiểm tra analyzer các file C# liên quan trả exit 0, có thông báo cảnh báo khi nạp workspace.

Project chưa có Makefile, test project hoặc scripts/check-docs.js. Các package NuGet.Packaging/NuGet.Protocol 6.12.1 được công cụ scaffold kéo gián tiếp hiện có cảnh báo NU1901; không tắt cảnh báo hoặc thay đổi dependency ngoài phạm vi scaffold.

Lỗi có sẵn ngoài phạm vi: trong locale hiện tại, ô Price của Product Edit hiển thị `10,00` nhưng jQuery yêu cầu dấu chấm và chặn Save. Đã tái hiện kết quả giống nhau ở bản mã nguồn trước khi scaffold và bản hiện tại; không thay đổi Product để che lỗi trong lần sửa này.
