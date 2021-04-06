import { __decorate } from "tslib";
import { Component } from '@angular/core';
export class FeedLine {
}
let FeedListComponent = class FeedListComponent {
    constructor() {
        this.displayedColumns = ['checkbox', 'feedTitle', 'datasourceTitle', 'nbFollowers', 'nbStoriesPerMonth'];
        this.feeds = [
            { feedTitle: 'News', datasourceTitle: 'BBC', nbFollowers: 10, nbStoriesPerMonth: 10 },
            { feedTitle: 'News', datasourceTitle: 'Sputnik', nbFollowers: 5, nbStoriesPerMonth: 2 }
        ];
    }
};
FeedListComponent = __decorate([
    Component({
        selector: 'app-feed-list',
        templateUrl: './feed-list.component.html',
        styleUrls: ['./feed-list.component.sass']
    })
], FeedListComponent);
export { FeedListComponent };
//# sourceMappingURL=feed-list.component.js.map