// ===== ANIMATIONS GLOBALES =====

// Intersection Observer pour les animations au scroll
const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -50px 0px'
};

const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add('animate-in');
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Observer tous les éléments avec classe d'animation
document.addEventListener('DOMContentLoaded', () => {
    // Animer les cartes
    document.querySelectorAll('.specialty-card, .action-card, .feature-card, .stat-item').forEach(el => {
        observer.observe(el);
    });

    // Animer les sections
    document.querySelectorAll('.specialties-header, .stats-section, .cta-section').forEach(el => {
        observer.observe(el);
    });
});

// ===== CAROUSEL ANIMATIONS =====
let currentSlide = 0;
const slides = document.querySelectorAll('.carousel-slide');
const dots = document.querySelectorAll('.carousel-dot');

function showSlide(n) {
    if (slides.length === 0) return;
    
    slides.forEach(slide => slide.classList.remove('active'));
    dots.forEach(dot => dot.classList.remove('active'));
    
    slides[n].classList.add('active');
    if (dots[n]) dots[n].classList.add('active');
}

function nextSlide() {
    if (slides.length === 0) return;
    currentSlide = (currentSlide + 1) % slides.length;
    showSlide(currentSlide);
}

function previousSlide() {
    if (slides.length === 0) return;
    currentSlide = (currentSlide - 1 + slides.length) % slides.length;
    showSlide(currentSlide);
}

function goToSlide(n) {
    if (slides.length === 0) return;
    currentSlide = n;
    showSlide(currentSlide);
}

// Auto-advance carousel
if (slides.length > 0) {
    setInterval(nextSlide, 5000);
}

// ===== COUNTER ANIMATIONS =====
function animateCounter(element, target) {
    let current = 0;
    const increment = target / 50;
    const timer = setInterval(() => {
        current += increment;
        if (current >= target) {
            element.textContent = target;
            clearInterval(timer);
        } else {
            element.textContent = Math.floor(current);
        }
    }, 30);
}

// Animate counters when visible
const counterObserver = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
        if (entry.isIntersecting && !entry.target.dataset.animated) {
            const target = parseInt(entry.target.dataset.target) || 0;
            animateCounter(entry.target, target);
            entry.target.dataset.animated = 'true';
            counterObserver.unobserve(entry.target);
        }
    });
}, { threshold: 0.5 });

document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('[data-target]').forEach(counter => {
        counterObserver.observe(counter);
    });
});

// ===== FILTER ANIMATIONS =====
function filterSpecialties(category) {
    const cards = document.querySelectorAll('.specialty-card');
    const buttons = document.querySelectorAll('.filter-btn');
    
    // Update active button
    buttons.forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');

    // Filter cards with animation
    cards.forEach(card => {
        if (category === 'all') {
            card.style.display = 'block';
            card.style.animation = 'slideInUp 0.5s ease-out';
        } else {
            card.style.display = 'none';
        }
    });
}

// ===== SMOOTH SCROLL =====
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

// ===== HOVER EFFECTS =====
document.addEventListener('DOMContentLoaded', () => {
    // Add hover animation to buttons
    document.querySelectorAll('.btn-specialty, .cta-button, .action-link').forEach(btn => {
        btn.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-2px)';
        });
        btn.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });

    // Add hover animation to cards
    document.querySelectorAll('.specialty-card, .action-card, .feature-card').forEach(card => {
        card.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-10px)';
        });
        card.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
});

// ===== SCROLL ANIMATIONS =====
window.addEventListener('scroll', () => {
    // Parallax effect for hero
    const hero = document.querySelector('.hero-carousel');
    if (hero) {
        const scrollPosition = window.pageYOffset;
        hero.style.backgroundPosition = `center ${scrollPosition * 0.5}px`;
    }
});

// ===== FADE IN ON LOAD =====
window.addEventListener('load', () => {
    document.body.style.opacity = '1';
    document.querySelectorAll('[data-animate]').forEach((el, index) => {
        setTimeout(() => {
            el.classList.add('animate-in');
        }, index * 100);
    });
});
