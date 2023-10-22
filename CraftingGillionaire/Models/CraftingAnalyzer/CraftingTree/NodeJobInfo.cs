namespace CraftingGillionaire.Models.CraftingAnalyzer
{
    public class NodeJobInfo
    {
        internal NodeJobInfo(string jobName, int jobLevel, bool userCanCraft) 
        { 
            this.JobName = jobName;
            this.JobLevel = jobLevel;
            this.UserCanCraft = userCanCraft;
        }

        public string JobName { get; }

        public int JobLevel { get; }

        public bool UserCanCraft { get; }
    }
}
