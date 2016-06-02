export default class EventAggregator {
    constructor() {
        this.subscribers = {};
    }
    
    subscribe(event, handler) {
        let subscriptions = this.subscribers[event];
        if (!subscriptions) {
            subscriptions = [];
            this.subscribers[event] = subscriptions;
        }
        subscriptions.push(handler);
    }
    
    unsubscribe(event, handler) {
        let subscriptions = this.subscribers[event];
        if (subscriptions) {
            let index = subscriptions.indexOf(handler);
            if (index >= 0) {
                subscriptions.splice(index, 1);   
            }            
        }
    }
    
    publish(event, eventArgs) {
        let subscriptions = this.subscribers[event];
        if (subscriptions) {
            for (let subscription of subscriptions) {
                subscription(eventArgs);
            }
        }
    }
}