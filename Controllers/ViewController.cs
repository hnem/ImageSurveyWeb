using Microsoft.AspNetCore.Mvc;

namespace ImageSurveyWeb.Controllers
{
    [ApiController]
    public class ViewController : ControllerBase
    {
        [HttpGet("/view/{fileName}")]
        public IActionResult ViewImage(string fileName)
        {
            var imageUrl = $"/images/{fileName}";

            var html = $@"
                <html>
                    <head>
                        <meta charset='UTF-8'>
                        <title>アンケートページ</title>
                    </head>
                    <body>
                        <h2>画像の確認とアンケート</h2>
                        <img src='{imageUrl}' alt='Uploaded Image' style='max-width:400px;' /><br/><br/>

                        <form action='/submit/{fileName}' method='post'>
                            <label>名前: <input type='text' name='name' required /></label><br/><br/>
                            <label>評価（1〜5）:
                                <select name='rating'>
                                    <option value='1'>1</option>
                                    <option value='2'>2</option>
                                    <option value='3'>3</option>
                                    <option value='4'>4</option>
                                    <option value='5'>5</option>
                                </select>
                            </label><br/><br/>
                            <label>コメント:<br/>
                            <textarea name='comment' rows='4' cols='40'></textarea></label><br/><br/>
                            <button type='submit'>送信</button>
                        </form>
                    </body>
                </html>
            ";

            return Content(html, "text/html");
        }

        [HttpPost("/submit/{fileName}")]
        public IActionResult Submit(
    string fileName,
    [FromForm] string name,
    [FromForm] string rating,
    [FromForm] string comment)
        {
            var imageUrl = $"/images/{fileName}";

            // 保存先フォルダとファイルパスを作成
            var saveDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
            Directory.CreateDirectory(saveDir);
            var jsonPath = Path.Combine(saveDir, fileName + ".json");

            // 回答データをオブジェクト化
            var response = new
            {
                fileName,
                name,
                rating,
                comment,
                timestamp = DateTime.Now.ToString("s")
            };

            // JSONとして保存
            var json = System.Text.Json.JsonSerializer.Serialize(response, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            System.IO.File.WriteAllText(jsonPath, json);

            // 回答完了ページ（画像ダウンロード付き）
            var html = $@"
                <html>
                    <head>
                        <meta charset='UTF-8'>
                        <title>アンケート送信完了</title>
                    </head>
                    <body>
                        <h2>ご回答ありがとうございました。</h2>

                        <p><strong>名前：</strong>{name}</p>
                        <p><strong>評価：</strong>{rating}</p>
                        <p><strong>コメント：</strong>{comment}</p>

                        <hr />
                        <a href='{imageUrl}' download>
                            <button>画像をダウンロード</button>
                        </a>
                    </body>
                </html>
            ";

            return Content(html, "text/html");
        }
    }
}
