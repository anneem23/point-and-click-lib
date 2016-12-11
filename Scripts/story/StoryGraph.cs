// ------------------------------------------------------------------------------
//  The storygraph intially only contains the default quest which only has one
//	condition to complete - find the next quest. 
//	
// 	The story graph will grow over time. When the player discovers clues an event
//  is dispatched containing the quest.
// ------------------------------------------------------------------------------
using System;
using PointAndClick.Event;
using PointAndClick.DataStructure;

namespace PointAndClick.Story {
	
	public class StoryGraph {
		// when the player finds the first quest the default postcondition is met and quest is completed
		private static Func<object, bool> DEFAULT_POSTCONDITION = x => story.Size() > 1;
		protected static Quest DEFAULT_QUEST = new Quest("Find a Quest.", null, DEFAULT_POSTCONDITION);
		private static Graph<Quest> story = new Graph<Quest>();

		// c# equivalent to java's static blocks are static contructors
		static StoryGraph() {
			story.AddNode (new GraphNode<Quest>(DEFAULT_QUEST));
			MessageQueue.register<QuestEventArgs> (new EventHandler(AddQuest));
		}

		public static Quest NextQuest() {
			return story.GetEnumerator().Current;
		}

		public static void AddQuest(object sender, EventArgs args) {
			QuestEventArgs eventArgs = (QuestEventArgs) args;
			story.AddNode(new GraphNode<Quest>(eventArgs.Quest));
		}

	}
}