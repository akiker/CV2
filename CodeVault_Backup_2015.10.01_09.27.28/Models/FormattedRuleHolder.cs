
namespace CodeVault.Models
{
    public class FormattedRuleHolder
    {
        public FormattedRuleHolder(int ruleId,string typeName,SccmRuleConnector connector,string clause)
        {
            this.RuleId = ruleId;
            this.TypeName = typeName;
            this.Connector = connector;
            this.Clause = clause;
        }
        public int RuleId { get; set; }
        public string TypeName { get; set; }
        public SccmRuleConnector Connector {get; set;}
        public string Clause { get; set; }
    }
}
