using MVP.Common;
using MVP.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MVP.Core.Business
{
    public class MVPBusiness
    {
        public static MVPResultModel GetMVPPlayer()
        {
            MVPResultModel model = new MVPResultModel();
            try
            {

                string[] lineText;
                List<Player> players = new List<Player>();

                Basketball oBasketball = null;
                List<Basketball> oBasketballList = new List<Basketball>();

                Handball oHandball = null;
                List<Handball> oHandballList = new List<Handball>();

                foreach (string txtName in Directory.GetFiles(Constant.ProjectRoot + "SportsData", "*.txt"))
                {
                    using (StreamReader sr = new StreamReader(txtName))
                    {
                        IEnumerable<string> sportData = System.IO.File.ReadLines(txtName);
                        Sports sportType = Helper.ParseEnum<Sports>(sportData.FirstOrDefault());
                        Player newPlayer = null;

                        #region Winner Team on File
                        //I must calculate winner every file upload time, because some player can be played another match, so In a match with the same team names, they may win or lose.
                        foreach (var item in sportData.Skip(1))
                        {
                            lineText = item.Split(';');
                            switch (sportType)
                            {
                                case Sports.Basketball:
                                    oBasketball = new Basketball
                                    {
                                        SportName = sportData.FirstOrDefault(),
                                        Number = Int32.Parse(lineText[2]),
                                        Team = lineText[3],
                                        Scores = Int32.Parse(lineText[5])
                                    };
                                    oBasketballList.Add(oBasketball);
                                    break;
                                case Sports.Handball:
                                    oHandball = new Handball
                                    {
                                        SportName = sportData.FirstOrDefault(),
                                        Number = Int32.Parse(lineText[2]),
                                        Team = lineText[3],
                                        GoalMade = Int32.Parse(lineText[5])
                                    };
                                    oHandballList.Add(oHandball);
                                    break;
                                default:
                                    break;
                            }
                        }

                        string winnerTeam = "";
                        switch (sportType)
                        {
                            case Sports.Basketball:
                                var teamAndScoreBasketBall = oBasketballList.GroupBy(g => g.Team).Select(s => new
                                {
                                    Team = s.Key,
                                    Score = s.Sum(sum => sum.Scores)
                                });
                                winnerTeam = teamAndScoreBasketBall.OrderByDescending(o => o.Score).Select(s => s.Team).FirstOrDefault();
                                break;
                            case Sports.Handball:
                                var teamAndScoreHandball = oHandballList.GroupBy(g => g.Team).Select(s => new
                                {
                                    Team = s.Key,
                                    Score = s.Sum(sum => sum.GoalMade)
                                });
                                winnerTeam = teamAndScoreHandball.OrderByDescending(o => o.Score).Select(s => s.Team).FirstOrDefault();
                                break;
                            default:
                                break;
                        }

                        oBasketballList.Clear();
                        oHandballList.Clear();
                        #endregion

                        //this loop is my global player list. 
                        foreach (var item in sportData.Skip(1))
                        {
                            lineText = item.Split(';');

                            //The same player can be in multiple document. That's why we have 2 loop
                            if (players.Where(w => w.Name == lineText[0] && w.NickName == lineText[1]).Any())
                            {
                                newPlayer = players.Where(w => w.Name == lineText[0] && w.NickName == lineText[1]).FirstOrDefault();
                            }
                            else
                            {
                                newPlayer = new Player
                                {
                                    Name = lineText[0],
                                    NickName = lineText[1],
                                };
                            }

                            switch (sportType)
                            {
                                case Sports.Basketball:
                                    oBasketball = new Basketball
                                    {
                                        SportName = sportData.FirstOrDefault(),
                                        Number = Int32.Parse(lineText[2]),
                                        Team = lineText[3],
                                        Position = Helper.ParseEnum<PositionsBasketball>(lineText[4]),
                                        Scores = Int32.Parse(lineText[5]),
                                        Rebounds = Int32.Parse(lineText[6]),
                                        Assists = Int32.Parse(lineText[7]),
                                        Winner = lineText[3] == winnerTeam ? true : false
                                    };
                                    newPlayer.Positions.Basketballs.Add(oBasketball);
                                    break;
                                case Sports.Handball:
                                    oHandball = new Handball
                                    {
                                        SportName = sportData.FirstOrDefault(),
                                        Number = Int32.Parse(lineText[2]),
                                        Team = lineText[3],
                                        Position = Helper.ParseEnum<PositionsHandball>(lineText[4]),
                                        GoalMade = Int32.Parse(lineText[5]),
                                        GoalRecieved = Int32.Parse(lineText[6]),
                                        Winner = lineText[3] == winnerTeam ? true : false
                                    };
                                    newPlayer.Positions.Handballs.Add(oHandball);
                                    break;
                                default:
                                    break;
                            }

                            if (!players.Where(w => w.Name == lineText[0] && w.NickName == lineText[1]).Any())
                            {
                                players.Add(newPlayer);
                            }
                        }
                    }
                }

                model.Player = players.OrderByDescending(o => o.MVPScore).FirstOrDefault();
            }
            catch (Exception exp)
            {
                model.Player = null;
                model.Message = "Error! We can't get MVP Player!";
            }

            return model;
        }
    }
}
