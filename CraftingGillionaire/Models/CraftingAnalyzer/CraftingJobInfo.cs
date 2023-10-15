namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class CraftingJobInfo
    {
        public CraftingJobInfo(string jobName, int jobLevel)
        {
            JobName = jobName;
            JobLevel = jobLevel;
        }

        public string JobName { get; }

        public int JobLevel { get; }
    }
}
