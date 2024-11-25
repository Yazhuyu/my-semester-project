var builder = WebApplication.CreateBuilder(args);

// ���U IHttpClientFactory
builder.Services.AddHttpClient();

// ���U Controllers �P Views
builder.Services.AddControllersWithViews();

// ���U Session �䴩
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // �]�w Session �L���ɶ�
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // �T�O GDPR �����������p�k�W��`
});

var app = builder.Build();

// �t�m HTTP �ШD�y���u
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // �w�]�� HSTS �Ȭ� 30 �ѡC���Ͳ������A�A�i��ݭn���ܦ��ȡA�аѾ\ https://aka.ms/aspnetcore-hsts
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// �ҥ� Session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
