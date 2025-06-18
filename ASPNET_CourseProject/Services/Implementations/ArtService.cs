using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Data.Models;

namespace ASPNET_CourseProject.Services.Implementations
{
    public class ArtService : IArtService
    {
        public Page<ArtDTO> GetArt(int curPage)
        {
            Page<ArtDTO> page = new Page<ArtDTO>();
            page.CurPage = curPage;
            page.Items = new List<ArtDTO>();

            page.MaxPage = (int)Math.Ceiling((double)(linksForTest.Count / page.ItemsPerPage));
            int start = curPage * page.ItemsPerPage;

            for (int i = start; i < start + page.ItemsPerPage && i < linksForTest.Count; i++)
            {
                page.Items.Add(
                    new ArtDTO()
                    {
                        Title = "placeholder",
                        FilePath = linksForTest[i]
                    }
                );
            }
            return page;
        }
        // temp and exists only for view tests until db
        private readonly List<string> linksForTest = new List<string>()
                {
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain1.webp",
                    "https://mdbcdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(73).webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://cdn.pixabay.com/photo/2015/02/25/17/56/cat-649164_1280.jpg",
                    "https://cdn.pixabay.com/photo/2018/10/11/12/31/cat-3739702_1280.jpg",
                    "https://media.istockphoto.com/id/2167033066/ru/%D1%84%D0%BE%D1%82%D0%BE/junger-h%C3%BCbscher-kater-im-wohnzimmer.webp?s=2048x2048&w=is&k=20&c=OaBP9__r2xXkcNigT3PzWM1CxZ4dyYHilWaf_7_rRds=",
                    "https://cdn.pixabay.com/photo/2018/01/19/15/29/cat-3092650_1280.jpg",
                    "https://cdn.pixabay.com/photo/2018/08/08/05/12/cat-3591348_1280.jpg6",
                    "https://cdn.pixabay.com/photo/2019/04/02/16/11/cat-4098058_1280.jpg",
                    "https://cdn.pixabay.com/photo/2016/07/13/12/58/dragons-1514416_1280.png",
                    "https://cdn.pixabay.com/photo/2017/01/09/06/16/dragon-1964746_1280.png",

                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain1.webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://mdbcdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(73).webp",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain1.webp",
                    "https://mdbcdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(73).webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://cdn.pixabay.com/photo/2015/02/25/17/56/cat-649164_1280.jpg",
                    "https://cdn.pixabay.com/photo/2018/10/11/12/31/cat-3739702_1280.jpg",
                    "https://media.istockphoto.com/id/2167033066/ru/%D1%84%D0%BE%D1%82%D0%BE/junger-h%C3%BCbscher-kater-im-wohnzimmer.webp?s=2048x2048&w=is&k=20&c=OaBP9__r2xXkcNigT3PzWM1CxZ4dyYHilWaf_7_rRds=",
                    "https://cdn.pixabay.com/photo/2018/01/19/15/29/cat-3092650_1280.jpg",
                    "https://cdn.pixabay.com/photo/2018/08/08/05/12/cat-3591348_1280.jpg6",
                    "https://cdn.pixabay.com/photo/2019/04/02/16/11/cat-4098058_1280.jpg",
                    "https://cdn.pixabay.com/photo/2016/07/13/12/58/dragons-1514416_1280.png",
                    "https://cdn.pixabay.com/photo/2017/01/09/06/16/dragon-1964746_1280.png",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain1.webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.redd.it%2F02g1pfot5ds41.jpg&f=1&nofb=1&ipt=574762deb1096182616fd453cf74181f0435de840d1d425ffd6258c253c20e54",
                    "https://mdbcdn.b-cdn.net/img/Photos/Vertical/mountain3.webp",
                    "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.pinimg.com%2Foriginals%2F34%2F1a%2F31%2F341a314750809408c2082bac303f68c7.jpg&f=1&nofb=1&ipt=e9111a74f781c2c27c82f12b7a9a99d9f57d6bd461c8b8bdb015abbf53ae4370",
                    "https://external-content.duckduckgo.com/iu/?u=http%3A%2F%2Fwww.autofish.net%2Fmirrors%2Fimages%2Fanimals%2Fcats%2Fcutekittydish.jpg&f=1&nofb=1&ipt=52d1d4d86a17ef8b5b44d73ef472810c7cb60f6960a57ada894abdf216f4ba26",
                    "https://mdbcdn.b-cdn.net/img/Photos/Horizontal/Nature/4-col/img%20(73).webp"
                };
    }
}
