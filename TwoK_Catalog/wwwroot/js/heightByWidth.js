const newsFeed = document.querySelector('.news-feed__view');
const reviewsFeed = document.querySelector('.reviews__view');

const newsFeedHeighByWidthRatio = parseFloat(getComputedStyle(newsFeed).height) / parseFloat(getComputedStyle(newsFeed).width);
const reviewsFeedHeighByWidthRatio = parseFloat(getComputedStyle(reviewsFeed).height) / parseFloat(getComputedStyle(reviewsFeed).width);

window.addEventListener('resize', () =>{
    let containerWidth = parseInt(getComputedStyle(newsFeed).width);
    let containerHeight = parseInt(containerWidth * newsFeedHeighByWidthRatio) + "px";
    newsFeed.style.height = containerHeight;

    containerWidth = parseInt(getComputedStyle(reviewsFeed).width);
    containerHeight = parseInt(containerWidth * reviewsFeedHeighByWidthRatio) + "px";
    reviewsFeed.style.height = containerHeight;
});