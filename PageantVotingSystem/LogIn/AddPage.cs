using System;

namespace PageantVotingSystem
{
    public class AddPage
    {
        public int choice { get; set; }
        public AddPage()
        {
            Console.WriteLine("Choose\n");
            Console.WriteLine("1-Add Event\n");
            Console.WriteLine("2-Add Contestant\n");
            Console.WriteLine("3-Add Judge\n");
            Console.WriteLine("4-Cancel\n");
            choice = int.Parse(Console.ReadLine());
            Choices(choice);

        }
        public void Choices(int choice)
        {
            if (choice == 1)
            {
                Console.WriteLine("Enter event details:");

                Console.Write("Event ID: ");
                int eventId = int.Parse(Console.ReadLine());

                Console.Write("Name: ");
                string eventName = Console.ReadLine();

                Console.Write("Description: ");
                string eventDescription = Console.ReadLine();

                Console.Write("Address: ");
                string eventAddress = Console.ReadLine();

                Console.Write("Date Scheduled: ");
                string eventDate = Console.ReadLine();

                Console.Write("Scoring System Type: ");
                string eventType = Console.ReadLine();

                Event events = new Event(eventId, eventName, eventDescription, eventAddress, eventDate, eventType);

                Console.Write("How many segments:");
                int countsegment = int.Parse(Console.ReadLine());

                AddSegment(events, countsegment);


                events.DisplayEvent();
            }
        }
        public void AddSegment(Event events, int i)
        {
            while (i > 0)
            {
                Console.WriteLine("\nEnter segment details:");
                Console.Write("Segment ID: ");
                int segmentId = int.Parse(Console.ReadLine());

                Console.Write("Segment Name: ");
                string segmentName = Console.ReadLine();

                Console.Write("Segment Description: ");
                string segmentDescription = Console.ReadLine();

                Console.Write("Segment Percentage Weight: ");
                double segmentWeight = double.Parse(Console.ReadLine());

                Console.Write("How many round in this segment: ");
                int countRound = int.Parse(Console.ReadLine());

                Segment segments = new Segment(segmentId, segmentName, segmentDescription, segmentWeight);

                events.AddSegment(segments);
                AddRound(segments, countRound);
                i--;
            }

        }
        public void AddRound(Segment segments, int i)
        {
            while (i > 0)
            {
                Console.WriteLine("\nEnter round details:");
                Console.Write("Round ID: ");
                int roundId = int.Parse(Console.ReadLine());

                Console.Write("Round Name: ");
                string roundName = Console.ReadLine();

                Console.Write("Round Description: ");
                string roundDescription = Console.ReadLine();

                Console.Write("Round weight: ");
                int roundDuration = int.Parse(Console.ReadLine());

                Console.Write("How many criteria in this round: ");
                int countCriteria = int.Parse(Console.ReadLine());

                Round rounds = new Round(roundId, roundName, roundDescription, roundDuration);

                segments.AddRound(rounds);
                AddCriteria(rounds, countCriteria);
                i--;
            }
        }
        public void AddCriteria(Round rounds, int i)
        {
            while (i > 0)
            {
                Console.WriteLine("\nEnter criteria details:");
                Console.Write("Criteria ID: ");
                int criteriaId = int.Parse(Console.ReadLine());

                Console.Write("Criteria Name: ");
                string criteriaName = Console.ReadLine();

                Console.Write("Criteria Percentage Weight: ");
                double criteriaWeight = double.Parse(Console.ReadLine());

                Console.Write("Criteria Maximum Score: ");
                double baseValue = double.Parse(Console.ReadLine());

                Criteria criteria = new Criteria(criteriaId, criteriaName, criteriaWeight, baseValue);
                rounds.AddCriteria(criteria);
                i--;
            }
        }
    }
}
