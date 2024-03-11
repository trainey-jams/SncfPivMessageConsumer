using SncfPivMessageConsumer.Models;
using SncfPivMessageConsumer.Models.PivMessage.Root;

namespace SncfPivMessageConsumer.Mappers;

public interface IPivMapper
{
    public Task<PivMessageRoot> MapAndTranslate(ActiveMQMessageWrapper rawMessage);

    public string Serialize(object obj, bool useLongNames);
}