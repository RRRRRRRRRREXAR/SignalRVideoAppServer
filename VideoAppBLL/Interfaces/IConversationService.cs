using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;

namespace VideoAppBLL.Interfaces
{
	public interface IConversationService
	{
		Task CreateConversation(ConversationDTO dialog);
		Task DeleteConversation(int ConversationId, UserDTO owner);
		Task<ConversationDTO> GetConversation(int ConversationId);
		Task<IEnumerable<ConversationDTO>> GetAllConversations(UserDTO user);
		Task RemoveUser(UserDTO user, UserDTO owner, ConversationDTO conversation);
	}
}
