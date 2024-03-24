using ÇekilişSitesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ÇekilişSitesi.Controllers
{
    public class RaffleController : Controller
    {

        public RaffleController()
        {
        }


		public IActionResult StartRaffle(IFormFile participantsFile, IFormFile prizesFile)
		{

			return View("DisplayScreen",StartRaffleProcess(participantsFile,prizesFile));
		}

		[HttpPost]
        public RaffleEntity StartRaffleProcess(IFormFile participantsFile, IFormFile prizesFile)
        {
			bool isMultiEntryEnabled = Request.Form.Keys.FirstOrDefault(x=> x == "isMultiEntryEnabled") != null;
            bool isMultiWinEnabled = Request.Form.Keys.FirstOrDefault(x => x == "isMultiWinEnabled") != null;


			if (participantsFile != null && prizesFile != null)
            {

                RaffleEntity raffleEntity = GenerateRaffleEntity(participantsFile, prizesFile, isMultiEntryEnabled, isMultiWinEnabled);

                GenerateResult(raffleEntity);

                return raffleEntity;
                //return RedirectToAction("DisplayScreen", raffleEntity); 
            }

            return new RaffleEntity();
            //return RedirectToAction("Error");
        }

        public RaffleEntity GenerateRaffleEntity(IFormFile participantsFile, IFormFile prizesFile, bool isMultiEntryEnabled, bool isMultiWinEnabled)
        {
            RaffleEntity result = new RaffleEntity();

            result.participants = ReadParticipantsFile(participantsFile, isMultiEntryEnabled);
            result.prizes = ReadPrizesFile(prizesFile);

            result.isMultipleWinEnabled = isMultiWinEnabled;
            result.isMultipleEntryAllowed = isMultiEntryEnabled;

            return result;
        }
        private List<Participant> ReadParticipantsFile(IFormFile participantsFile, bool isMultiEntryEnabled)
        {
            if (participantsFile != null && participantsFile.Length > 0)
                try
                {
                    var streamReader = new StreamReader(participantsFile.OpenReadStream());
                    return GenerateParticipants(streamReader, isMultiEntryEnabled);

                }
                catch (Exception) { return new List<Participant>(); }

            return new List<Participant>();
        }

        private List<Participant> GenerateParticipants(StreamReader streamReader, bool isMultiEntryEnabled)
        {
            List<Participant> result = new List<Participant>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var participantDatas = line.Split(',', StringSplitOptions.TrimEntries);

                Participant participant = new Participant(participantDatas);

                if (isMultiEntryEnabled && participant.CoefficientFactor > 1) //I can make another method for it, so it would be more faster and less complicated but idc that much.
                {
                    short factor = participant.CoefficientFactor;
                    participant.CoefficientFactor = 1;
                    for (int i = 0; i < factor; i++)
                        result.Add( new Participant(participant.FullName,1, participant.PhoneNumber, participant.MailAddress, participant.Handle) );
                }
                else
                    result.Add(participant);
            }

            return result;
        }

        private List<Prize> ReadPrizesFile(IFormFile prizesFile)
        {
            if (prizesFile != null && prizesFile.Length > 0)
                try
                {
                    var streamReader = new StreamReader(prizesFile.OpenReadStream());
                    return GeneratePrizes(streamReader);

                }
                catch (Exception) { return new List<Prize>(); }

            return new List<Prize>();
        }

        private List<Prize> GeneratePrizes(StreamReader streamReader)
        {
            List<Prize> result = new List<Prize>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var prizeDatas = line.Split(',', StringSplitOptions.TrimEntries);

                Prize prize = new Prize(prizeDatas);
				
                if (prize.Quantity > 1)
				{
					short factor = prize.Quantity;
					prize.Quantity = 1;
					for (int i = 0; i < factor; i++)
						result.Add(new Prize(prize.PrizeName));
				}
				else
					result.Add(prize);
			}

            return result;
        }

        internal void GenerateResult(RaffleEntity raffleEntity)
        {
            if (raffleEntity.isMultipleWinEnabled)
            {
                Random random = new Random();
                for (int i = 0; i < raffleEntity.prizes.Count; i++)
                {
                    raffleEntity.participants[random.Next(0, raffleEntity.participants.Count)].Prizes.Add(raffleEntity.prizes[i]);

                }
                    

            }
            else
            {
                UniqueRandomNumberGenerator generator = new UniqueRandomNumberGenerator(0, raffleEntity.participants.Count - 1);

                for (int i = 0; i < raffleEntity.prizes.Count; i++)
                    raffleEntity.participants[generator.GetUniqueRandomNumber()].Prizes.Add(raffleEntity.prizes[i]);

            }

            raffleEntity.participants = raffleEntity.participants.Where(x => x.Prizes.Count() > 0).ToList();
            // <-- PLAN B -->
            //else
            //{
            //    Random random = new Random();
            //    List<int> winnerPositions = new List<int>();

            //    int winnerPosition = -1;
            //    for (int i = 0; i < raffleEntity.prizes.Count; i++)
            //    {
            //        while (winnerPosition == -1 || winnerPositions.Contains(winnerPosition)) //That might be problematic in bigger numbers but works fine until few hundred?
            //            winnerPosition = random.Next(0, raffleEntity.participants.Count);

            //        winnerPositions.Add(winnerPosition);
            //    }

            //    for (int i = 0; i < winnerPositions.Count; i++)
            //        raffleEntity.participants[winnerPositions[i]].Prizes.Add(raffleEntity.prizes[i]);
            //}
        }

    }
}
