using Microsoft.EntityFrameworkCore;
using vphone.Models;

namespace vphone.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new QLQuanDTContext(serviceProvider.GetRequiredService<DbContextOptions<QLQuanDTContext>>()))
            {
                context.Database.EnsureCreated();
                if (context.Users.Any())
                {
                    return;
                }
                var users = new User[]
                {
                    new User{Id = 1, Name = "Nguyễn Văn Triệu", Email="admin@gmail.com", Password = "123456", Address="Nam Định", Phone="0123456789"},
                    new User{Id = 2, Name = "Nguyễn Danh Trường", Email="truong@gmail.com", Password = "123456", Address="Hà Nội", Phone="0123456789"},
                    new User{Id = 3, Name = "Nguyễn Tùng Lâm", Email="lam@gmail.com", Password = "123456", Address="Hà Nội", Phone="0123456789"},
                    new User{Id = 4, Name = "Bùi Xuân Nam", Email="nam@gmail.com", Password = "123456", Address="Hà Nội", Phone="0123456789"},
                    new User{Id = 5, Name = "Vũ Đức Lân", Email="lan@gmail.com", Password = "123456", Address="Hà Nội", Phone="0123456789"},
                };
                foreach (var user in users)
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();
                if (context.Categories.Any())
                {
                    return;
                }
                var categories = new Category[]
                {
                    new Category{Id = 1, Title = "Iphone", Slug ="Iphone", Description = "Điện thoại Iphone" ,UserId=1},
                    new Category{Id = 2, Title = "Samsung", Slug ="Samsung", Description = "Điện thoại Samsung" ,UserId=1 },
                    new Category{Id = 3, Title = "Xiaomi", Slug ="Xiaomi", Description = "Điện thoại Xiaomi" ,UserId=1 }

                };
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();

                if (context.Products.Any())
                {
                    return;
                }
                var products = new Product[]
                {
                    new Product{Id = 1 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro Max (512GB) - Chính hãng VN/A", Price = 39750000,IsStock = true, CatId = 1, Image = "iphone-15-pro-max-blue-titanium-pure.png", Description = "iPhone 15 Pro Max đem đến cho người dùng đa dạng sự lựa chọn với ba phiên bản bộ nhớ trong lần lượt là 256GB/512GB/1TB và bốn lựa chọn màu gồm Titan Tự Nhiên/Titan Trắng/Titan Xanh/Titan Đen. Ngoài việc sử dụng chất liệu Titan mới, những cải tiến về cấu hình được Apple cập nhật và trang bị hứa hẹn đem đến trải nghiệm người dùng nâng cao hơn.", Slug = "Dien-thoai-di-dong-iphone-15-pro-max-512GB-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1290 x 2796, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = "6.7\"", OperatingSystem = "iOS 17", Processor = "A17 Pro", InternalMemory = "512GB", Ram = "8GB", BatteryCapacity = "4.422mAh"},
                    new Product{Id = 2 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro Max (256GB) - Chính hãng VN/A", Price = 33850000,IsStock = true, CatId = 1, Image = "iphone-15-pro-max-natural-titanium-pure.png", Description = "iPhone 15 Pro Max đem đến cho người dùng đa dạng sự lựa chọn với ba phiên bản bộ nhớ trong lần lượt là 256GB/512GB/1TB và bốn lựa chọn màu gồm Titan Tự Nhiên/Titan Trắng/Titan Xanh/Titan Đen. Ngoài việc sử dụng chất liệu Titan mới, những cải tiến về cấu hình được Apple cập nhật và trang bị hứa hẹn đem đến trải nghiệm người dùng nâng cao hơn.", Slug = "Dien-thoai-di-dong-iphone-15-pro-max-256GB-chinh-hang-VNA", IsFeatured = true, ScreenTech = " Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1290 x 2796, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = "6.7\"", OperatingSystem = "iOS 17", Processor = "A17 Pro", InternalMemory = "256GB", Ram = "8GB", BatteryCapacity = "4.422mAh"},
                    new Product{Id = 3 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro Max (1T) - Chính hãng VN/A", Price = 45990000,IsStock = true, CatId = 1, Image = "iphone-15-pro-max-white-titanium-pure.png", Description = "iPhone 15 Pro Max đem đến cho người dùng đa dạng sự lựa chọn với ba phiên bản bộ nhớ trong lần lượt là 256GB/512GB/1TB và bốn lựa chọn màu gồm Titan Tự Nhiên/Titan Trắng/Titan Xanh/Titan Đen. Ngoài việc sử dụng chất liệu Titan mới, những cải tiến về cấu hình được Apple cập nhật và trang bị hứa hẹn đem đến trải nghiệm người dùng nâng cao hơn.", Slug = "Dien-thoai-di-dong-iphone-15-pro-max-1T-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1290 x 2796, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = "6.7\"", OperatingSystem = "iOS 17", Processor = "A17 Pro", InternalMemory = "1TB", Ram = "8GB", BatteryCapacity = "4.422mAh"},
                    new Product{Id = 4 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro (1TB) - Chính hãng VN/A", Price = 42490000,IsStock = true, CatId = 1, Image = "iphone-15-pro-natural-titanium-pure.png", Description = "iPhone 15 Pro sở hữu màn hình Super Retina XDR OLED 6,1 inch với tần số quét 120Hz và độ sáng lên tới 2000 nit. Bên cạnh đó, với con chip A17 Pro mạnh mẽ, máy mang đến hiệu năng cao cùng khả năng xử lý đa nhiệm tuyệt vời. Về camera, iPhone 15 Pro được trang bị cụm camera gồm 3 ống kính: 48MP cho góc rộng, 12MP cho tele và góc siêu rộng cao cấp.", Slug = "Dien-thoai-di-dong-iphone-15-pro-1T-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1179 x 2556, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = " 6.1\"", OperatingSystem = " iOS 17", Processor = "A17 Pro", InternalMemory = "1TB", Ram = "8GB", BatteryCapacity = "3.274 mAh" },
                    new Product{Id = 5 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro (512GB) - Chính hãng VN/A", Price = 36490000,IsStock = true, CatId = 1, Image = "iphone-15-pro-white-titanium-pure.png", Description = "iPhone 15 Pro sở hữu màn hình Super Retina XDR OLED 6,1 inch với tần số quét 120Hz và độ sáng lên tới 2000 nit. Bên cạnh đó, với con chip A17 Pro mạnh mẽ, máy mang đến hiệu năng cao cùng khả năng xử lý đa nhiệm tuyệt vời. Về camera, iPhone 15 Pro được trang bị cụm camera gồm 3 ống kính: 48MP cho góc rộng, 12MP cho tele và góc siêu rộng cao cấp.", Slug = "Dien-thoai-di-dong-iphone-15-pro-512GB-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1179 x 2556, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = "6.1\"", OperatingSystem = "iOS 17", Processor = "A17 Pro", InternalMemory = "512GB", Ram = "8GB", BatteryCapacity = "3.274 mAh"},
                    new Product{Id = 6 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro (256GB) - Chính hãng VN/A", Price = 29990000,IsStock = true, CatId = 1, Image = "iphone-15-pro-black-titanium-pure.png", Description = "iPhone 15 Pro sở hữu màn hình Super Retina XDR OLED 6,1 inch với tần số quét 120Hz và độ sáng lên tới 2000 nit. Bên cạnh đó, với con chip A17 Pro mạnh mẽ, máy mang đến hiệu năng cao cùng khả năng xử lý đa nhiệm tuyệt vời. Về camera, iPhone 15 Pro được trang bị cụm camera gồm 3 ống kính: 48MP cho góc rộng, 12MP cho tele và góc siêu rộng cao cấp.", Slug = "Dien-thoai-di-dong-iphone-15-pro-256GB-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = " 1179 x 2556, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = "6.1\"", OperatingSystem = "iOS 17", Processor = "A17 Pro", InternalMemory = "256GB", Ram = "8GB", BatteryCapacity = "3.274 mAh"},
                    new Product{Id = 7 ,UserId=1, Name = "Điện thoại di động iPhone 15 Pro (128GB) - Chính hãng VN/A", Price = 27490000,IsStock = true, CatId = 1, Image = "iphone-15-pro-blue-titanium-pure.png", Description = "iPhone 15 Pro sở hữu màn hình Super Retina XDR OLED 6,1 inch với tần số quét 120Hz và độ sáng lên tới 2000 nit. Bên cạnh đó, với con chip A17 Pro mạnh mẽ, máy mang đến hiệu năng cao cùng khả năng xử lý đa nhiệm tuyệt vời. Về camera, iPhone 15 Pro được trang bị cụm camera gồm 3 ống kính: 48MP cho góc rộng, 12MP cho tele và góc siêu rộng cao cấp.", Slug = "Dien-thoai-di-dong-iphone-15-pro-128GB-chinh-hang-VNA", IsFeatured = true, ScreenTech = "Màn hình Super Retina XDR, Tấm nền OLED, Dynamic Island, Công nghệ ProMotion với tốc độ làm mới thích ứng lên đến 120Hz, Màn hình Luôn Bật, Màn hình HDR, Tỷ lệ tương phản 2.000.000:1 (tiêu chuẩn), Màn hình True Tone, Màn hình có dải màu rộng (P3), Haptic Touch", Resolution = "1179 x 2556, Chính: 48MP, khẩu độ ƒ/1.78, Ultra Wide: 12MP, khẩu độ ƒ/2.2, Telephoto: 12MP, khẩu độ ƒ/2.8, Camera trước TrueDepth 12MP, khẩu độ ƒ/1.9", ScreenSize = " 6.1\"", OperatingSystem = " iOS 17", Processor = "A17 Pro", InternalMemory = "128GB", Ram = "8GB", BatteryCapacity = "3.274 mAh" },
                      new Product{Id = 8,
                        UserId=1,
                        Name ="Điện thoại di động  Samsung Galaxy Z Fold5",
                        Price = 32990000,
                        IsStock = true,
                        CatId = 2,
                        Image = "zfold5.png",
                        Description = "#",
                        Slug = "Galaxy Z Fold5",
                        IsFeatured = false,
                        ScreenTech = "Dynamic AMOLED 2X",
                        Resolution = "2176 x 1812 with 2316 x 904",
                        ScreenSize = "7.6 inch and 6,7 inch",
                        OperatingSystem = "OneUI 5.1.1 / Android 13",
                        Processor = "Snapdragon® 8 Gen 2",
                        InternalMemory = "256GB",
                        Ram = "12GB",
                        BatteryCapacity = "4400 mAh"
                    },
                     new Product{Id = 9,
                        UserId=1,
                        Name ="Điện thoại di động Xiaomi 12T (8GB/128GB) - Chính hãng",
                        Price = 9880000,
                        IsStock = true,
                        CatId = 3,
                        Image = "xiaomi12t.png",
                        Description = "#",
                        Slug = "Xiaomi 12T",
                        IsFeatured = false,
                        ScreenTech = "AMOLED DotDisplay",
                        Resolution = "1220x2712",
                        ScreenSize = "6,67 inch",
                        OperatingSystem = "MIUI 13, Android 12",
                        Processor = "Dimensity 8100-Ultra",
                        InternalMemory = "128GB",
                        Ram = "8GB",
                        BatteryCapacity ="5000mAh"
                    },

                };
                foreach (var product in products)
                {
                    context.Products.Add(product);
                }
                context.SaveChanges();
                if (context.Orders.Any())
                {
                    return;
                }
                var orders = new Order[]
                {
                    new Order{Id = 1, UserId = 1, Name ="Cù Trọng Xoay", Email = "cu@gmail.com" ,Phone="0386673152", Address="Xuân Trường, Nam Định", State=false},
                    new Order{Id = 2, UserId = 1, Name ="Lò Thị Nở", Email = "no@gmail.com" ,Phone="0386673155" , Address="Hà Nội", State=false},
                    new Order{Id = 3, UserId = 1, Name ="Xuân Tóc Đỏ", Email = "xuan@gmail.com" ,Phone="0386673151", Address="Phú Thọ", State=true}

                };
                foreach (var order in orders)
                {
                    context.Orders.Add(order);
                }
                context.SaveChanges();
                if (context.OrderDetails.Any())
                {
                    return;
                }
                var orderDetails = new OrderDetail[]
                {
                    new OrderDetail{Id=1, Name = "Điện thoại di động iPhone 15 Pro Max (512GB) - Chính hãng VN/A", Image = "iphone-15-pro-max-blue-titanium-pure.png", PrdId = 1, OrderId =1, Qty=4, Price = 39750000},
                    new OrderDetail{Id=2, Name = "Điện thoại di động iPhone 15 Pro Max (1T) - Chính hãng VN/A", Image = "iphone-15-pro-max-white-titanium-pure.png",  PrdId = 3, OrderId =1, Qty=2, Price = 45990000},
                    new OrderDetail{Id=3, Name = "Điện thoại di động iPhone 15 Pro Max (512GB) - Chính hãng VN/A", Image = "iphone-15-pro-max-blue-titanium-pure.png", PrdId = 1, OrderId =2, Qty=3, Price = 39750000},
                    new OrderDetail{Id=4, Name = "Điện thoại di động iPhone 15 Pro Max (256GB) - Chính hãng VN/A", Image = "iphone-15-pro-max-natural-titanium-pure.png",  PrdId = 2, OrderId =2, Qty=2, Price = 33850000},
                    new OrderDetail{Id=5, Name = "Điện thoại di động iPhone 15 Pro Max (256GB) - Chính hãng VN/A", Image = "iphone-15-pro-max-natural-titanium-pure.png",  PrdId = 2, OrderId =3, Qty=1, Price = 33850000},
                    new OrderDetail{Id=6, Name = "Điện thoại di động iPhone 15 Pro Max (1T) - Chính hãng VN/A", Image = "iphone-15-pro-max-white-titanium-pure.png",  PrdId = 3, OrderId =3, Qty=2, Price = 45990000},

                };
                foreach (var orderDetail in orderDetails)
                {
                    context.OrderDetails.Add(orderDetail);
                }
                context.SaveChanges();
            }
        }
    }
}
