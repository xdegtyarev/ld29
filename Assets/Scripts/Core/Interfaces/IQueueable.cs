public interface IQueueable {
	void OnEnqueue(BasementQueue queue);
	void OnDequeue(BasementQueue queue);
}