using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mahjong
{


    /// <summary>
    /// 消息中心
    /// </summary>
    public class MessageCenter : Singleton<MessageCenter>
    {
        // 消息委托
        public delegate void MessageDelegate(object body);


        public delegate void MsgListener(string msg);

        // 消息字典
        private Dictionary<string, List<MessageDelegate>> messageMap = new Dictionary<string, List<MessageDelegate>>();

        // 消息监听
        private Dictionary<object, MsgListener> msgListeners = new Dictionary<object, MsgListener>();


        /// <summary>
        /// 构造函数
        /// 避免外界new
        /// </summary>
        // private MessageCenter() { }

        /// <summary>
        /// 注册监听
        /// </summary>
        public void AddListener(string messageType, MessageDelegate messageDelegate)
        {
            if (messageDelegate == null) return;
            if (!messageMap.ContainsKey(messageType))
            {
                messageMap.Add(messageType, new List<MessageDelegate>());
            }
            List<MessageDelegate> list = messageMap[messageType];
            if (!list.Contains(messageDelegate))
            {
                list.Add(messageDelegate);
            }
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        public void RemoveListener(string messageType, MessageDelegate messageDelegate)
        {
            if (messageDelegate == null) return;
            if (messageMap.ContainsKey(messageType))
            {
                List<MessageDelegate> list = messageMap[messageType];
                list.Remove(messageDelegate);
            }
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        public void Clear()
        {
            messageMap.Clear();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageName">消息类型 </param>
        /// <param name="body"> 发送消息主体 </param>
        public void Dispatch(string messageType, object body = null)
        {
            if (messageMap.ContainsKey(messageType))
            {
                // Message evt = new Message(messageType, body);
                try
                {
                    List<MessageDelegate> list = messageMap[messageType];
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i](body);
                    }
                    if (this.msgListeners.Count > 0)
                    {
                        foreach (var key in this.msgListeners.Keys)
                        {
                            this.msgListeners[key](messageType);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("SendMessage:" + messageType.ToString() + e.Message + e.StackTrace + e);
                }
            }

        }
        public void AddMsgListenr(object obj, MsgListener listener)
        {
            this.msgListeners.Add(obj, listener);
        }
        public void RemoveMsgListenr(object obj)
        {
            if (this.msgListeners.ContainsKey(obj))
            {
                this.msgListeners.Remove(obj);
            }

        }
        public bool ContainsMsgListener(object obj)
        {
            if (this.msgListeners.ContainsKey(obj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}