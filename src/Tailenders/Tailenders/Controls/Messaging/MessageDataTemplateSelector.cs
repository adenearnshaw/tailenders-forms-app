using System;
using Tailenders.ViewModels;
using Xamarin.Forms;

namespace Tailenders.Controls.Messaging
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingTemplate;
        DataTemplate outgoingTemplate;

        public MessageDataTemplateSelector()
        {
            incomingTemplate = new DataTemplate(typeof(IncomingMessageViewCell));
            outgoingTemplate = new DataTemplate(typeof(OutgoingMessageViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ConversationItemViewModel)
            {
                if (((ConversationItemViewModel)item).IsOutgoing)
                    return outgoingTemplate;
                else
                    return incomingTemplate; 
            }

            throw new Exception($"Unknown chat");
        }
    }
}
