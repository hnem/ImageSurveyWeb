var builder = WebApplication.CreateBuilder(args);

// サービス登録
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// パイプライン設定
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();   // ← 後で使う画像URL返却のため追加（先に入れてもOK）
app.UseRouting();

app.UseAuthorization(); // ← 将来ログイン処理入れるなら必要

app.MapControllers();   // ← Controllerルート登録

app.Run();
