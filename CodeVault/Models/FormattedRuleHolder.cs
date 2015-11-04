namespace CodeVault.Models
{
    public class FormattedRuleHolder
    {
        public FormattedRuleHolder(int ruleId, string typeName, SccmRuleConnector connector, string clause)
        {
            RuleId = ruleId;
            TypeName = typeName;
            Connector = connector;
            Clause = clause;
        }

        public int RuleId { get; set; }
        public string TypeName { get; set; }
        public SccmRuleConnector Connector { get; set; }
        public string Clause { get; set; }
    }
}