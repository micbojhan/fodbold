using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Presentation.Web.Controllers
{
    public class TestController : Controller
    {
        private bool IsBusy = false;

        //http://ionicframework.com/docs/v2/ 

        public async Task GetBlah()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var client = new HttpClient();
                var json = await client.GetStringAsync("http://api.heroesofnewerth.com/player_statistics/ranked/accountid/123456/?token=ZQ8BGO658NMXHPR1");
                var items = JsonConvert.DeserializeObject<RootObject>(json);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<ActionResult> GetGames()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("UserId", "21");
            var json = await httpClient.GetStringAsync("https://muuvme2.azurewebsites.net/api/games");
            var items = JsonConvert.DeserializeObject<List<Games>>(json);
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetStream(int userId, int gameId)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://muuvme2.azurewebsites.net/api/stream?userId=" + userId + "&gameId=" + gameId);
            var items = JsonConvert.DeserializeObject<Stream>(json);
            var streams = items.StreamItems.Select(stream => new MuuvMeStream
            {
                Error = stream == null,
                StreamId = stream?.StreamItemId,
                Message = stream?.Message,

                FromUserName = stream?.FromUser?.UserName,
                ToUserName = stream?.ToUser?.UserName,

                TrackArtistPicture = stream?.TrackItem?.ArtistPicture,
                TrackAlbumCover = stream?.TrackItem?.AlbumCover,
                TrackArtistName = stream?.TrackItem?.ArtistName,
                TrackTitle = stream?.TrackItem?.TrackTitle,
                TrackLink = stream?.TrackItem?.TrackLink,

                GuessComment = stream?.Guess?.Comment,
                GuessArtistCorrect = stream?.Guess?.ArtistGuessCorrect,
                GuessTrackCorrect = stream?.Guess?.TrackGuessCorrect,
            }).ToList();

            return Json(streams, JsonRequestBehavior.AllowGet);
        }



        //public async Task TagBillede()
        //{
        //    await CrossMedia.Current.PickPhotoAsync();
        //}
    }

    public class MuuvMeStream
    {
        public bool Error { get; set; }
        public int? StreamId { get; set; }
        public string Message { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string TrackArtistPicture { get; set; }
        public string TrackAlbumCover { get; set; }
        public string TrackArtistName { get; set; }
        public string TrackTitle { get; set; }
        public string TrackLink { get; set; }

        public string GuessComment { get; set; }
        public bool? GuessTrackCorrect { get; set; }
        public bool? GuessArtistCorrect { get; set; }

    }

    public class Games
    {
        public int Id { get; set; }
        public int OpponentId { get; set; }
        public string Title { get; set; }
        public object UpdatedAt { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string TimeZoneId { get; set; }
        public int? AspNetUserId { get; set; }
    }

    public class Guess
    {
        public int GuessId { get; set; }
        public bool GuessSent { get; set; }
        public string Comment { get; set; }
        public bool? ArtistGuessCorrect { get; set; }
        public bool? TrackGuessCorrect { get; set; }
    }

    public class AudioElement
    {
        public int TrackId { get; set; }
        public string PreviewUrl { get; set; }
        public bool SmallButtons { get; set; }
        public bool PreviewNotAllowed { get; set; }
    }

    public class TrackItem
    {
        public object RosettaCatalog { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
        public object ArtistLink { get; set; }
        public string ArtistPicture { get; set; }
        public string AlbumTitle { get; set; }
        public int AlbumId { get; set; }
        public string AlbumCover { get; set; }
        public object SpotifyAlbumTitle { get; set; }
        public int SpotifyAlbumId { get; set; }
        public string TrackTitle { get; set; }
        public string TrackLink { get; set; }
        public bool UseTargetLink { get; set; }
        public bool ShowSongInformation { get; set; }
        public AudioElement AudioElement { get; set; }
        public object SpotifyAudioElement { get; set; }
        public object InitialComment { get; set; }
        public object NewGameTitle { get; set; }
        public bool UserContext { get; set; }
        public object artist_foreign_ids { get; set; }
        public object ArtistForeignIds { get; set; }
        public object AlbumForeignIds { get; set; }
        public object TrackForeignIds { get; set; }
        public object DeezerTrack_foreign_id { get; set; }
        public object MusicBrainzTrack_foreign_id { get; set; }
        public double Match { get; set; }
    }

    public class NewSearch
    {
        public int CardId { get; set; }
        public int CreatedByUserId { get; set; }
        public int SendToUserId { get; set; }
        public string SendToUserName { get; set; }
        public object SendToUserList { get; set; }
        public int SelectedTrackId { get; set; }
        public object SelectedEchoNestArtistId { get; set; }
        public object SelectedArtistName { get; set; }
        public object SelectedSongId { get; set; }
        public object SelectedEchoNestTrackId { get; set; }
        public object InitialComment { get; set; }
        public bool Validated { get; set; }
        public object SearchText { get; set; }
        public object SearchModel { get; set; }
        public object SelectedDeezerTrackId { get; set; }
        public object SelectedTrack { get; set; }
        public object NewGameTitle { get; set; }
        public bool FirstBlindTaste { get; set; }
    }

    public class StreamItem
    {
        public int StreamItemId { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string DatePart { get; set; }
        public string TimePart { get; set; }
        public int CardId { get; set; }
        public object NewMessage { get; set; }
        public int GameId { get; set; }
        public User FromUser { get; set; }
        public User ToUser { get; set; }
        public List<object> Messages { get; set; }
        public Guess Guess { get; set; }
        public object GuessComment { get; set; }
        public TrackItem TrackItem { get; set; }
        public NewSearch NewSearch { get; set; }
    }
    public class Stream
    {
        public List<StreamItem> StreamItems { get; set; }
        public int GameId { get; set; }
        public int CurrentUserId { get; set; }
        public string NewMessage { get; set; }
        public int NewMessageForUserId { get; set; }
        public object TakeCount { get; set; }
        public object FirstBlindTaste { get; set; }
    }



    public class RootObject
    {
        public string account_id { get; set; }
        public string rnk_games_played { get; set; }
        public string rnk_wins { get; set; }
        public string rnk_losses { get; set; }
        public string rnk_concedes { get; set; }
        public string rnk_concedevotes { get; set; }
        public string rnk_buybacks { get; set; }
        public string rnk_discos { get; set; }
        public string rnk_kicked { get; set; }
        public string rnk_amm_solo_rating { get; set; }
        public string rnk_amm_solo_count { get; set; }
        public string rnk_amm_solo_conf { get; set; }
        public string rnk_amm_solo_prov { get; set; }
        public string rnk_amm_solo_pset { get; set; }
        public string rnk_amm_team_rating { get; set; }
        public string rnk_amm_team_count { get; set; }
        public string rnk_amm_team_conf { get; set; }
        public string rnk_amm_team_prov { get; set; }
        public string rnk_amm_team_pset { get; set; }
        public string rnk_herokills { get; set; }
        public string rnk_herodmg { get; set; }
        public string rnk_heroexp { get; set; }
        public string rnk_herokillsgold { get; set; }
        public string rnk_heroassists { get; set; }
        public string rnk_deaths { get; set; }
        public string rnk_goldlost2death { get; set; }
        public string rnk_secs_dead { get; set; }
        public string rnk_teamcreepkills { get; set; }
        public string rnk_teamcreepdmg { get; set; }
        public string rnk_teamcreepexp { get; set; }
        public string rnk_teamcreepgold { get; set; }
        public string rnk_neutralcreepkills { get; set; }
        public string rnk_neutralcreepdmg { get; set; }
        public string rnk_neutralcreepexp { get; set; }
        public string rnk_neutralcreepgold { get; set; }
        public string rnk_bdmg { get; set; }
        public string rnk_bdmgexp { get; set; }
        public string rnk_razed { get; set; }
        public string rnk_bgold { get; set; }
        public string rnk_denies { get; set; }
        public string rnk_exp_denied { get; set; }
        public string rnk_gold { get; set; }
        public string rnk_gold_spent { get; set; }
        public string rnk_exp { get; set; }
        public string rnk_actions { get; set; }
        public string rnk_secs { get; set; }
        public string rnk_consumables { get; set; }
        public string rnk_wards { get; set; }
        public string rnk_em_played { get; set; }
        public string rnk_level { get; set; }
        public string rnk_level_exp { get; set; }
        public string rnk_min_exp { get; set; }
        public string rnk_max_exp { get; set; }
        public string rnk_time_earning_exp { get; set; }
        public string rnk_bloodlust { get; set; }
        public string rnk_doublekill { get; set; }
        public string rnk_triplekill { get; set; }
        public string rnk_quadkill { get; set; }
        public string rnk_annihilation { get; set; }
        public string rnk_ks3 { get; set; }
        public string rnk_ks4 { get; set; }
        public string rnk_ks5 { get; set; }
        public string rnk_ks6 { get; set; }
        public string rnk_ks7 { get; set; }
        public string rnk_ks8 { get; set; }
        public string rnk_ks9 { get; set; }
        public string rnk_ks10 { get; set; }
        public string rnk_ks15 { get; set; }
        public string rnk_smackdown { get; set; }
        public string rnk_humiliation { get; set; }
        public string rnk_nemesis { get; set; }
        public string rnk_retribution { get; set; }
        public object nickname { get; set; }
    }
}