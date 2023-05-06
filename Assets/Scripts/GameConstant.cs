using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class GameConstant
    {
        // Tổng số mạng mặc định
        public const int defaultLives = 3;

        // Số mạng còn lại
        public const string livesRest = "lives";

        // Số điểm hiện tại
        public const string currentScore = "current_score";

        // Điểm cao nhất
        public const string highScore = "high_score";

        // Tổng điểm sau các vòng
        public const string stageScore = "stage_score";
    }

    // Gọi highscore bằng cách viết PlayerPref.GetInt(GameConstant.highScore)
    // Đặt highscore = value bằng cách viết PlayerPref.SetInt(GameConstant.highScore, value)
}
