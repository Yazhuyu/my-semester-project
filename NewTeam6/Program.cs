var builder = WebApplication.CreateBuilder(args);

// 註冊 IHttpClientFactory
builder.Services.AddHttpClient();

// 註冊 Controllers 與 Views
builder.Services.AddControllersWithViews();

// 註冊 Session 支援
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 設定 Session 過期時間
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // 確保 GDPR 或類似的隱私法規遵循
});

var app = builder.Build();

// 配置 HTTP 請求流水線
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // 預設的 HSTS 值為 30 天。對於生產場景，你可能需要改變此值，請參閱 https://aka.ms/aspnetcore-hsts
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 啟用 Session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
