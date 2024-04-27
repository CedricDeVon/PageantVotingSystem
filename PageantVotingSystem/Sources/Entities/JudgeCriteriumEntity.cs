
namespace PageantVotingSystem.Sources.Entities
{
    public class JudgeCriteriumEntity
    {
        public ResultEntity Result { get; set; }

        public CriteriumEntity Criterium { get; set; }

        public JudgeCriteriumEntity()
        {
            SetAttributes(new ResultEntity(), new CriteriumEntity());
        }

        public JudgeCriteriumEntity(
            ResultEntity resultEntity,
            CriteriumEntity criteriumEntity)
        {
            SetAttributes(resultEntity, criteriumEntity);
        }

        private void SetAttributes(
            ResultEntity resultEntity = null,
            CriteriumEntity criteriumEntity = null)
        {
            Result = resultEntity;
            Criterium = criteriumEntity;
        }
    }
}
