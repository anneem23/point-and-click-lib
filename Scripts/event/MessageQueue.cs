
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace PointAndClick.Event {

	public class MessageQueue  {
		
		private static Dictionary<Type, EventHandler> eventHandlers = new Dictionary<Type, EventHandler>();
		
		public static void register<T>(EventHandler subscriber) where T : EventArgs {
			if (eventHandlers.ContainsKey (typeof(T))) {
				eventHandlers [typeof(T)] += subscriber;
			} else {
				eventHandlers.Add(typeof(T), subscriber);
			}
		}

		public static void unregister<T>(EventHandler e) where T : EventArgs {
			eventHandlers [typeof(T)] -= e;
		}
		
		public static void dispatchEvent<T> (object publisher, T args) where T : EventArgs {
			EventHandler handler = null;
			eventHandlers.TryGetValue (typeof(T), out handler);

			if (handler != null) {
				handler (publisher, args);
			}
		}
	}

}